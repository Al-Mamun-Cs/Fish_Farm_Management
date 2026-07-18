using MediatR;
using SchoolManagement.Application.DTOs.Upozilas;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Commands
{
    public class CreateUpozilaCommand : IRequest<BaseCommandResponse>
    {
        public CreateUpozilaDto UpozilaDto { get; set; }
    }
}
