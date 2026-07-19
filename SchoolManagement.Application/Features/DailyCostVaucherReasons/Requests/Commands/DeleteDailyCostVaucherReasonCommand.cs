using MediatR;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands
{
    public class DeleteDailyCostVaucherReasonCommand : IRequest
    {
        public int DailyCostVaucherReasonId { get; set; }
    }
}
