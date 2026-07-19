using MediatR;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands
{
    public class CreateDailyCostVaucherReasonCommand : IRequest<BaseCommandResponse>
    {
        public CreateDailyCostVaucherReasonDto DailyCostVaucherReasonDto { get; set; }
    }
}
