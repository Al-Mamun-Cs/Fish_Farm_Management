using MediatR;
using SchoolManagement.Application.DTOs.FishSales;

namespace SchoolManagement.Application.Features.FishSales.Requests.Queries
{
    public class GetFishSaleDetailRequest : IRequest<FishSaleDto>
    {
        public int FishSaleId { get; set; }
    }
}
