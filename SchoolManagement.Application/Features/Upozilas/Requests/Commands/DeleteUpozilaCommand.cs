using MediatR;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Commands
{
    public class DeleteUpozilaCommand : IRequest
    {
        public int UpazilaId { get; set; }
    }
}
