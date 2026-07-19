using MediatR;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands
{
    public class CreateDailyMiscellaneousCostCommand : IRequest<BaseCommandResponse>
    {
        public CreateDailyMiscellaneousCostDto DailyMiscellaneousCostDto { get; set; }
    }
}
