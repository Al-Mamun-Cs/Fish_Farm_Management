using MediatR;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands
{
    public class UpdateDailyMiscellaneousCostCommand : IRequest<Unit>
    {
        public DailyMiscellaneousCostDto DailyMiscellaneousCostDto { get; set; }
    }
}
