using MediatR;
using SchoolManagement.Application.DTOs.FishSales;

namespace SchoolManagement.Application.Features.FishSales.Requests.Commands
{
    public class UpdateFishSaleCommand : IRequest<Unit>
    {
        public FishSaleDto FishSaleDto { get; set; }
    }
}
