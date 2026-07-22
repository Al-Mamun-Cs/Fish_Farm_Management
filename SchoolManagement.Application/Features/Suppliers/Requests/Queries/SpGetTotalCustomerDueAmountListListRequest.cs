using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class SpGetTotalCustomerDueAmountListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
