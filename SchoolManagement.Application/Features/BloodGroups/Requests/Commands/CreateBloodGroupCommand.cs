using MediatR;
using SchoolManagement.Application.DTOs.BloodGroups;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.BloodGroups.Requests.Commands
{
    public class CreateBloodGroupCommand : IRequest<BaseCommandResponse>
    {
        public CreateBloodGroupDto BloodGroupDto { get; set; }
    }
}
