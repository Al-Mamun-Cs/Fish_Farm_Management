using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Commands
{
    public class DeleteCompanyInvestorReturnCommandHandler : IRequestHandler<DeleteCompanyInvestorReturnCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCompanyInvestorReturnCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCompanyInvestorReturnCommand request, CancellationToken cancellationToken)
        {
            var CompanyInvestorReturn = await _unitOfWork.Repository<CompanyInvestorReturn>().Get(request.CompanyInvestorReturnId);

            if (CompanyInvestorReturn == null)
                throw new NotFoundException(nameof(CompanyInvestorReturn), request.CompanyInvestorReturnId);


            try
            {
                await _unitOfWork.Repository<CompanyInvestorReturn>().Delete(CompanyInvestorReturn);

                if (CompanyInvestorReturn.Type == 1)
                {
                    var companyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Get(CompanyInvestorReturn?.CompanyInvestorId ?? 0);
                    companyInvestor.ReturnInvestAmount -= (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<CompanyInvestor>().Update(companyInvestor);

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(CompanyInvestorReturn?.WarehouseId ?? 0);
                    warehouse.CashInHand += (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);
                }
                else
                {
                    var companyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Get(CompanyInvestorReturn?.CompanyInvestorId ?? 0);
                    companyInvestor.ProfitAmount -= (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<CompanyInvestor>().Update(companyInvestor);

                    var warehouse = await _unitOfWork.Repository<Warehouse>().Get(CompanyInvestorReturn?.WarehouseId ?? 0);
                    warehouse.CashInHand += (CompanyInvestorReturn.Amount);
                    await _unitOfWork.Repository<Warehouse>().Update(warehouse);

                }

                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.CompanyInvestorReturnId);
            }

            return Unit.Value;
        }
    }
}
