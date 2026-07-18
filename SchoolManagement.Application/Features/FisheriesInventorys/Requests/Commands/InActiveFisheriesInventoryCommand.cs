using MediatR;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands
{
    public class InActiveFisheriesInventoryCommand : IRequest 
    {
        public int FisheriesInventoryId { get; set; }
    }
}
