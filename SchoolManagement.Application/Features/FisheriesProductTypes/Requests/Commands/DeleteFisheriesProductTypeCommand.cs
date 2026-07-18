using MediatR;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands
{
    public class DeleteFisheriesProductTypeCommand : IRequest
    {
        public int FisheriesProductTypeId { get; set; }
    }
}
