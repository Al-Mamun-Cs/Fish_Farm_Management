using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries
{
    public class SpGetDailyAssetCostTotalRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
