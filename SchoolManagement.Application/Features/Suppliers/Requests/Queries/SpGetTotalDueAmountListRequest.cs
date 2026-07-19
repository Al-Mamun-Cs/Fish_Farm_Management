using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class SpGetTotalDueAmountListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
