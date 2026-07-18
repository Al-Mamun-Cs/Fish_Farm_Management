using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Queries
{
    public class GetAutoCompleteProductNameRequestHandler : IRequestHandler<GetAutoCompleteProductNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FisheriesInventoryDetail> _FisheriesInventoryDetailRepository;
        public GetAutoCompleteProductNameRequestHandler(ISchoolManagementRepository<FisheriesInventoryDetail> FisheriesInventoryDetailRepository)
        {
            _FisheriesInventoryDetailRepository = FisheriesInventoryDetailRepository;
        }
        private readonly ISchoolManagementRepository<FisheriesInventory> _fisheriesInventoryRepository;

        public GetAutoCompleteProductNameRequestHandler(
            ISchoolManagementRepository<FisheriesInventoryDetail> fisheriesInventoryDetailRepository,
            ISchoolManagementRepository<FisheriesInventory> fisheriesInventoryRepository)
        {
            _FisheriesInventoryDetailRepository = fisheriesInventoryDetailRepository;
            _fisheriesInventoryRepository = fisheriesInventoryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetAutoCompleteProductNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<FisheriesInventoryDetail> traineeBioDataGeneralInfos =
                await _FisheriesInventoryDetailRepository.FilterAsync(x =>
                    (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) &&
                    x.FisheriesProductTypeId == request.FisheriesProductTypeId &&
                    x.AvailableQty > 0 &&
                    x.ProductName.Contains(request.ProductName));

            var selectModels = new List<SelectedModel>();

            foreach (var item in traineeBioDataGeneralInfos)
            {
                var inventory = await _fisheriesInventoryRepository.Get(item.FisheriesInventoryId ?? 0);

                selectModels.Add(new SelectedModel
                {
                    Text = $"{item.ProductName} - মজুদের পরিমাণ: {item.AvailableQty:0.00} - ক্রয়ের তারিখ {inventory.PurchaseDate:dd MMM yyyy}",
                    Value = item.FisheriesInventoryDetailId
                });
            }

            return selectModels;
        }
    }
}
