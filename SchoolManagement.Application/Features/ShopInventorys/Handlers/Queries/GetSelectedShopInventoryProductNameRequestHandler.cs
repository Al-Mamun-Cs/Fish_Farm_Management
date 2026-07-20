using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShopInventoryDetails.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.shopInventoryDetails.Handlers.Queries
{
    public class GetSelectedShopInventoryProductNameRequestHandler : IRequestHandler<GetSelectedShopInventoryProductNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ShopInventoryDetail> _shopInventoryDetailRepository;


        public GetSelectedShopInventoryProductNameRequestHandler(ISchoolManagementRepository<ShopInventoryDetail> shopInventoryDetailRepository)
        {
            _shopInventoryDetailRepository = shopInventoryDetailRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShopInventoryProductNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<ShopInventoryDetail> codeValues = await _shopInventoryDetailRepository.FilterAsync(x =>x.WarehouseId == request.WarehouseId && x.FisheriesProductTypeId ==request.FisheriesProductTypeId && x.AvailableQty > 0);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ProductName + " - " + (x.CostingPrice ?? 0).ToString("0.00") + " - মজুদ : " + (x.AvailableQty ?? 0).ToString("0.00"),
                Value = x.ShopInventoryDetailId
            }).ToList();
            return selectModels;
        }
    }
}
