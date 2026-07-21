using MediatR;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands
{
    public class InActiveDailyMiscellaneousCostCommand : IRequest 
    {
        public int DailyMiscellaneousCostId { get; set; }
    }
}
