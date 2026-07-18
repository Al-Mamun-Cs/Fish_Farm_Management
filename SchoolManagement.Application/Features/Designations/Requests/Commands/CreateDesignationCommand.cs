using MediatR;
using SchoolManagement.Application.DTOs.Designations;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Designations.Requests.Commands
{
    public class CreateDesignationCommand : IRequest<BaseCommandResponse>
    {
        public CreateDesignationDto DesignationDto { get; set; }
    }
}
