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

        public async Task<List<SelectedModel>> Handle(GetAutoCompleteProductNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<FisheriesInventoryDetail> codeValues = await _FisheriesInventoryDetailRepository.FilterAsync(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) &&
                          x.FisheriesProductTypeId == request.FisheriesProductTypeId &&
                           x.AvailableQty > 0);
            List < SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ProductName + " - মজুদের পরিমাণ: " + x.AvailableQty,
                Value = x.FisheriesInventoryDetailId
            }).ToList();
            return selectModels;
        }
    }
    //public class GetAutoCompleteProductNameRequestHandler : IRequestHandler<GetAutoCompleteProductNameRequest, List<SelectedModel>>
    //{
    //    private readonly ISchoolManagementRepository<FisheriesInventoryDetailDetail> _FisheriesInventoryDetailDetailRepository;
    //    public GetAutoCompleteProductNameRequestHandler(ISchoolManagementRepository<FisheriesInventoryDetailDetail> FisheriesInventoryDetailDetailRepository)
    //    {
    //        _FisheriesInventoryDetailDetailRepository = FisheriesInventoryDetailDetailRepository;
    //    }
    //    private readonly ISchoolManagementRepository<FisheriesInventoryDetail> _FisheriesInventoryDetailRepository;

    //    public GetAutoCompleteProductNameRequestHandler(
    //        ISchoolManagementRepository<FisheriesInventoryDetailDetail> FisheriesInventoryDetailDetailRepository,
    //        ISchoolManagementRepository<FisheriesInventoryDetail> FisheriesInventoryDetailRepository)
    //    {
    //        _FisheriesInventoryDetailDetailRepository = FisheriesInventoryDetailDetailRepository;
    //        _FisheriesInventoryDetailRepository = FisheriesInventoryDetailRepository;
    //    }

    //    public async Task<List<SelectedModel>> Handle(GetAutoCompleteProductNameRequest request, CancellationToken cancellationToken)
    //    {
    //        ICollection<FisheriesInventoryDetailDetail> traineeBioDataGeneralInfos =
    //            await _FisheriesInventoryDetailDetailRepository.FilterAsync(x =>
    //                (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) &&
    //                x.FisheriesProductTypeId == request.FisheriesProductTypeId &&
    //                x.AvailableQty > 0 &&
    //                x.ProductName.Contains(request.ProductName));

    //        var selectModels = new List<SelectedModel>();

    //        foreach (var item in traineeBioDataGeneralInfos)
    //        {
    //            var inventory = await _FisheriesInventoryDetailRepository.Get(item.FisheriesInventoryDetailId ?? 0);

    //            selectModels.Add(new SelectedModel
    //            {
    //                Text = $"{item.ProductName} - মজুদের পরিমাণ: {item.AvailableQty:0.00} - ক্রয়ের তারিখ {inventory.PurchaseDate:dd MMM yyyy}",
    //                Value = item.FisheriesInventoryDetailDetailId
    //            });
    //        }

    //        return selectModels;
    //    }
    //}
}
