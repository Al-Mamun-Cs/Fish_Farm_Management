using MediatR;

namespace SchoolManagement.Application.Features.FishSales.Requests.Commands
{
    public class DeleteFishSaleCommand : IRequest
    {
        public int FishSaleId { get; set; }
    }
}
