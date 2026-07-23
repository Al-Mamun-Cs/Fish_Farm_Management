using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries
{
    public class SpGetDailyAssetCostDetailListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
