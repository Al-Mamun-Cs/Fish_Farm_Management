using MediatR;
using SchoolManagement.Application.DTOs.BloodGroups;

namespace SchoolManagement.Application.Features.BloodGroups.Requests.Commands
{
    public class UpdateBloodGroupCommand : IRequest<Unit>
    {
        public BloodGroupDto BloodGroupDto { get; set; }
    }
}
