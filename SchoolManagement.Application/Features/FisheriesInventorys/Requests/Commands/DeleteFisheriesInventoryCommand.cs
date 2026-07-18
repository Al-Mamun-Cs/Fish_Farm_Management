using MediatR;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands
{
    public class DeleteFisheriesInventoryCommand : IRequest
    {
        public int FisheriesInventoryId { get; set; }
    }
}
