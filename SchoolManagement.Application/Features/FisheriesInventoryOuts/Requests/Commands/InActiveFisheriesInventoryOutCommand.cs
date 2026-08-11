using MediatR;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands
{
    public class InActiveFisheriesInventoryOutCommand : IRequest 
    {
        public int FisheriesInventoryOutId { get; set; }
    }
}
