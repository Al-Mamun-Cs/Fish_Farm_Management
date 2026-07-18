using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Warehouses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Queries
{
    public class GetSelectedWarehouseByIdRequestHandler : IRequestHandler<GetSelectedWarehouseByIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Warehouse> _WarehouseRepository;


        public GetSelectedWarehouseByIdRequestHandler(ISchoolManagementRepository<Warehouse> WarehouseRepository)
        {
            _WarehouseRepository = WarehouseRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedWarehouseByIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Warehouse> codeValues = await _WarehouseRepository.FilterAsync(x => (request.WarehouseId == 0 || x.WarehouseId != request.WarehouseId));
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.WarehouseName,
                Value = x.WarehouseId
            }).ToList();
            return selectModels;
        }
    }
}
