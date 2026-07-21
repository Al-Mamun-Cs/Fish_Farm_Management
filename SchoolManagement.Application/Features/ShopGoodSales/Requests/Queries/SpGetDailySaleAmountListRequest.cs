using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries
{
    public class SpGetDailySaleAmountListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
