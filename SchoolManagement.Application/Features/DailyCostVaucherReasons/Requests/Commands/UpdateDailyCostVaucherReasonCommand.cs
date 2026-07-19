using MediatR;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands
{
    public class UpdateDailyCostVaucherReasonCommand : IRequest<Unit>
    {
        public DailyCostVaucherReasonDto DailyCostVaucherReasonDto { get; set; }
    }
}
