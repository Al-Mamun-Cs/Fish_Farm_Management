using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetSelectedSupplierRequestHandler : IRequestHandler<GetSelectedSupplierRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;


        public GetSelectedSupplierRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSupplierRequest request, CancellationToken cancellationToken)
        {
            ICollection<Supplier> codeValues = await _SupplierRepository.FilterAsync(x => (request.WarehouseId == 0 || x.WarehouseId == request.WarehouseId) && x.SupplierStatus == request.SupplierStatus );
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SupplierName,
                Value = x.SupplierId
            }).ToList();
            return selectModels;
        }
    }
}
