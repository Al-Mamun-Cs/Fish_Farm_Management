using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries
{
    public class GetSelectedCompanyInvestorRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
