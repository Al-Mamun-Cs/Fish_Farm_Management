using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Queries
{
    public class GetSelectedCompanyInvestorReturnRequestHandler : IRequestHandler<GetSelectedCompanyInvestorReturnRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<CompanyInvestorReturn> _CompanyInvestorReturnRepository;


        public GetSelectedCompanyInvestorReturnRequestHandler(ISchoolManagementRepository<CompanyInvestorReturn> CompanyInvestorReturnRepository)
        {
            _CompanyInvestorReturnRepository = CompanyInvestorReturnRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCompanyInvestorReturnRequest request, CancellationToken cancellationToken)
        {
            ICollection<CompanyInvestorReturn> codeValues = await _CompanyInvestorReturnRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Remarks,
                Value = x.CompanyInvestorReturnId
            }).ToList();
            return selectModels;
        }
    }
}
