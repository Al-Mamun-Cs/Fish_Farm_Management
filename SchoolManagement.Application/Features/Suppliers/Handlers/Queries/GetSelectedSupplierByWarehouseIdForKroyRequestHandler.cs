using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetSelectedSupplierByWarehouseIdForKroyRequestHandler : IRequestHandler<GetSelectedSupplierByWarehouseIdForKroyRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;


        public GetSelectedSupplierByWarehouseIdForKroyRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSupplierByWarehouseIdForKroyRequest request, CancellationToken cancellationToken)
        {
            ICollection<Supplier> codeValues = await _SupplierRepository.FilterAsync(x => x.WarehouseId==request.WarehouseId && x.SupplierStatus==2);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SupplierName+"-"+ x.PhoneNo+"-"+ x.Address,
                Value = x.SupplierId
            }).ToList();
            return selectModels;
        }
    }
}
