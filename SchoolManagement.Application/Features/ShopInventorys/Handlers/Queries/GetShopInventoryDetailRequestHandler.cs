using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Queries
{
    public class GetShopInventoryDetailRequestHandler : IRequestHandler<GetShopInventoryDetailRequest, ShopInventoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShopInventory> _ShopInventoryRepository;
        public GetShopInventoryDetailRequestHandler(ISchoolManagementRepository<ShopInventory> ShopInventoryRepository, IMapper mapper)
        {
            _ShopInventoryRepository = ShopInventoryRepository;
            _mapper = mapper;
        }
        

        public async Task<ShopInventoryDto> Handle(GetShopInventoryDetailRequest request, CancellationToken cancellationToken)
        {
            var ShopInventory = await _ShopInventoryRepository.FindOneAsync(
            x => x.ShopInventoryId == request.ShopInventoryId);


            return _mapper.Map<ShopInventoryDto>(ShopInventory);
        }
    }
}
