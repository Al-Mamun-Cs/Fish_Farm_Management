using MediatR;

namespace SchoolManagement.Application.Features.Genders.Requests.Commands
{
    public class DeleteGenderCommand : IRequest
    {
        public int GenderId { get; set; }
    }
}
