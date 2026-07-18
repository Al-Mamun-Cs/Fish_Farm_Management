using MediatR;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands
{
    public class UpdateFisheriesProductTypeCommand : IRequest<Unit>
    {
        public FisheriesProductTypeDto FisheriesProductTypeDto { get; set; }
    }
}
