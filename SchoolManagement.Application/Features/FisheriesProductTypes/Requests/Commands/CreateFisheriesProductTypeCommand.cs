using MediatR;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands
{
    public class CreateFisheriesProductTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateFisheriesProductTypeDto FisheriesProductTypeDto { get; set; }
    }
}
