using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Queries
{
    public class GetSelectedCompanyInvestorRequestHandler : IRequestHandler<GetSelectedCompanyInvestorRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<CompanyInvestor> _CompanyInvestorRepository;


        public GetSelectedCompanyInvestorRequestHandler(ISchoolManagementRepository<CompanyInvestor> CompanyInvestorRepository)
        {
            _CompanyInvestorRepository = CompanyInvestorRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCompanyInvestorRequest request, CancellationToken cancellationToken)
        {
            ICollection<CompanyInvestor> codeValues = await _CompanyInvestorRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.FullName,
                Value = x.CompanyInvestorId
            }).ToList();
            return selectModels;
        }
    }
}
