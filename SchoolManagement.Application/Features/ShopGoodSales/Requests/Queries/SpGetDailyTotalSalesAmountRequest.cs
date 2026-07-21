using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries
{
    public class SpGetDailyTotalSalesAmountRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
