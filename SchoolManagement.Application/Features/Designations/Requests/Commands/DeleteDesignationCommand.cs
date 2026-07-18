using MediatR;

namespace SchoolManagement.Application.Features.Designations.Requests.Commands
{
    public class DeleteDesignationCommand : IRequest
    {
        public int DesignationId { get; set; }
    }
}
