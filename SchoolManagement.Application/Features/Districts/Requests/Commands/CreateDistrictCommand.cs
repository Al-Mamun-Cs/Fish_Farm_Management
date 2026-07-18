using MediatR;
using SchoolManagement.Application.DTOs.Districts;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Districts.Requests.Commands
{
    public class CreateDistrictCommand : IRequest<BaseCommandResponse>
    {
        public CreateDistrictDto DistrictDto { get; set; }
    }
}
