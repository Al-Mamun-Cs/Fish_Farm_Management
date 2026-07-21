using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.Warehouses.Requests.Queries
{
    public class SpGetCashCapitalDetailRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
