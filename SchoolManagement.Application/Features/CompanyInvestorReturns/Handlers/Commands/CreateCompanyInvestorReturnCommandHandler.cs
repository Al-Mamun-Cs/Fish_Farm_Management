using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns.Validators;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Commands
{
    public class CreateCompanyInvestorReturnCommandHandler : IRequestHandler<CreateCompanyInvestorReturnCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCompanyInvestorReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCompanyInvestorReturnCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCompanyInvestorReturnDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompanyInvestorReturnDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var CompanyInvestorReturn = _mapper.Map<CompanyInvestorReturn>(request.CompanyInvestorReturnDto);

                CompanyInvestorReturn = await _unitOfWork.Repository<CompanyInvestorReturn>().Add(CompanyInvestorReturn);
                if (CompanyInvestorReturn.Type == 1)
                {
                    var companyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Get(CompanyInvestorReturn?.CompanyInvestorId ?? 0);
                    companyInvestor.ReturnInvestAmount += (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<CompanyInvestor>().Update(companyInvestor);

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(CompanyInvestorReturn?.WarehouseId ?? 0);
                    warehouse.CashInHand -= (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var companyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Get(CompanyInvestorReturn?.CompanyInvestorId ?? 0);
                    companyInvestor.ProfitAmount += (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<CompanyInvestor>().Update(companyInvestor);

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(CompanyInvestorReturn?.WarehouseId ?? 0);
                    warehouse.CashInHand -= (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }
                    

                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = CompanyInvestorReturn.CompanyInvestorReturnId;
            }

            return response;
        }
    }
}
