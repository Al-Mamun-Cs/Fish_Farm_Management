using MediatR;
using SchoolManagement.Application.DTOs.Depositors;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Depositors.Requests.Commands
{
    public class CreateDepositorCommand : IRequest<BaseCommandResponse>
    {
        public CreateDepositorDto DepositorDto { get; set; }
    }
}
