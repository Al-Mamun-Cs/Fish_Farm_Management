using MediatR;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands
{
    public class DeleteDailyMiscellaneousCostCommand : IRequest
    {
        public int DailyMiscellaneousCostId { get; set; }
    }
}
