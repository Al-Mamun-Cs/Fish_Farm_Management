using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries
{
    public class GetSelectedCompanyInvestorReturnRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
