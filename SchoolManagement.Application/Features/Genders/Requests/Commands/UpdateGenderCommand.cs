using MediatR;
using SchoolManagement.Application.DTOs.Genders;

namespace SchoolManagement.Application.Features.Genders.Requests.Commands
{
    public class UpdateGenderCommand : IRequest<Unit>
    {
        public GenderDto GenderDto { get; set; }
    }
}
