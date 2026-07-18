using MediatR;

namespace SchoolManagement.Application.Features.Divisions.Requests.Commands
{
    public class DeleteDivisionCommand : IRequest
    {
        public int DivisionId { get; set; }
    }
}
