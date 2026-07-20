using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopGoodSales;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Queries
{
    public class GetShopGoodSaleDetailRequestHandler : IRequestHandler<GetShopGoodSaleDetailRequest, ShopGoodSaleDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShopGoodSale> _ShopGoodSaleRepository;
        public GetShopGoodSaleDetailRequestHandler(ISchoolManagementRepository<ShopGoodSale> ShopGoodSaleRepository, IMapper mapper)
        {
            _ShopGoodSaleRepository = ShopGoodSaleRepository;
            _mapper = mapper;
        }
        

        public async Task<ShopGoodSaleDto> Handle(GetShopGoodSaleDetailRequest request, CancellationToken cancellationToken)
        {
            var ShopGoodSale = await _ShopGoodSaleRepository.FindOneAsync(
            x => x.ShopGoodSaleId == request.ShopGoodSaleId);


            return _mapper.Map<ShopGoodSaleDto>(ShopGoodSale);
        }
    }
}
