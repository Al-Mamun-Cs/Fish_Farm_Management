using MediatR;

namespace SchoolManagement.Application.Features.BloodGroups.Requests.Commands
{
    public class DeleteBloodGroupCommand : IRequest
    {
        public int BloodGroupId { get; set; }
    }
}
