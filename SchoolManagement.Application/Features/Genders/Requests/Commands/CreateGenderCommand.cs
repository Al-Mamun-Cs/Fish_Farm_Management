using MediatR;
using SchoolManagement.Application.DTOs.Genders;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Genders.Requests.Commands
{
    public class CreateGenderCommand : IRequest<BaseCommandResponse>
    {
        public CreateGenderDto GenderDto { get; set; }
    }
}
