using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FishSales.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FishSales.Handlers.Queries
{
    public class GetSelectedFishSaleRequestHandler : IRequestHandler<GetSelectedFishSaleRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FishSale> _FishSaleRepository;


        public GetSelectedFishSaleRequestHandler(ISchoolManagementRepository<FishSale> FishSaleRepository)
        {
            _FishSaleRepository = FishSaleRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFishSaleRequest request, CancellationToken cancellationToken)
        {
            ICollection<FishSale> codeValues = await _FishSaleRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SaleDate,
                Value = x.FishSaleId
            }).ToList();
            return selectModels;
        }
    }
}
