using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries
{
    public class SpGetDailyFishFeedConsumptionRequest : IRequest<DataTable>
    {
        public int? ProjectScheduleId { get; set; }

    }
}
