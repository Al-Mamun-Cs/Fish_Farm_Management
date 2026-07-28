using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries
{
    public class SpGetTotalInvestorRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
