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
            ICollection<FisheriesInventoryDetail> codeValues = await _FisheriesInventoryDetailRepository.FilterAsync(x => 
            (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) &&
            x.FisheriesProductTypeId == request.FisheriesProductTypeId &&
            x.ProjectTypeId == request.ProjectTypeId &&
            x.AvailableQty > 0);
            List < SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ProductName + " - মজুদের পরিমাণ: " + x.AvailableQty,
                Value = x.FisheriesInventoryDetailId
            }).ToList();
            return selectModels;
        }
    }
    
}
