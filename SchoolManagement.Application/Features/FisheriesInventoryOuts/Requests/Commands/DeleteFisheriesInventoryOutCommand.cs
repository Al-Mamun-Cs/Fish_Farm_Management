using MediatR;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands
{
    public class DeleteFisheriesInventoryOutCommand : IRequest
    {
        public int FisheriesInventoryOutId { get; set; }
    }
}
