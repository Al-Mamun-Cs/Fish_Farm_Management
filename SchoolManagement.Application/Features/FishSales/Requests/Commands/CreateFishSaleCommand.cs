using MediatR;
using SchoolManagement.Application.DTOs.FishSales;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.FishSales.Requests.Commands
{
    public class CreateFishSaleCommand : IRequest<BaseCommandResponse>
    {
        public CreateFishSaleDto FishSaleDto { get; set; }
    }
}
