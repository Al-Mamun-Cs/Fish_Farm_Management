using MediatR;
using SchoolManagement.Application.DTOs.Divisions;

namespace SchoolManagement.Application.Features.Divisions.Requests.Commands
{
    public class UpdateDivisionCommand : IRequest<Unit>
    {
        public DivisionDto DivisionDto { get; set; }
    }
}
