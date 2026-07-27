using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CompanyInvestors.Validators;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Commands
{
    public class CreateCompanyInvestorCommandHandler : IRequestHandler<CreateCompanyInvestorCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCompanyInvestorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCompanyInvestorCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCompanyInvestorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CompanyInvestorDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var CompanyInvestor = _mapper.Map<CompanyInvestor>(request.CompanyInvestorDto);

                CompanyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Add(CompanyInvestor);
                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                //await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = CompanyInvestor.CompanyInvestorId;
            }

            return response;
        }
    }
}
