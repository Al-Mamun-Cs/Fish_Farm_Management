using MediatR;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries
{
    public class GetDailyMiscellaneousCostDetailRequest : IRequest<DailyMiscellaneousCostDto>
    {
        public int DailyMiscellaneousCostId { get; set; }
    }
}
