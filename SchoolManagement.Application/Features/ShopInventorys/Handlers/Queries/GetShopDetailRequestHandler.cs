using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Queries
{
    public class GetShopDetailRequestHandler : IRequestHandler<GetShopDetailRequest, ShopInventoryDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Domain.ShopInventoryDetail> _ShopInventoryDetailRepository;
        public GetShopDetailRequestHandler(ISchoolManagementRepository<Domain.ShopInventoryDetail> ShopInventoryDetailRepository, IMapper mapper)
        {
            _ShopInventoryDetailRepository = ShopInventoryDetailRepository;
            _mapper = mapper;
        }
        

        public async Task<ShopInventoryDetailDto> Handle(GetShopDetailRequest request, CancellationToken cancellationToken)
        {
            var ShopInventory = await _ShopInventoryDetailRepository.FindOneAsync(
            x => x.ShopInventoryDetailId == request.ShopInventoryDetailId);


            return _mapper.Map<ShopInventoryDetailDto>(ShopInventory);
        }
    }
}
