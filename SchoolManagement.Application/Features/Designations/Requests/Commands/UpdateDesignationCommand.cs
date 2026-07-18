using MediatR;
using SchoolManagement.Application.DTOs.Designations;

namespace SchoolManagement.Application.Features.Designations.Requests.Commands
{
    public class UpdateDesignationCommand : IRequest<Unit>
    {
        public DesignationDto DesignationDto { get; set; }
    }
}
