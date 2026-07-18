using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetSelectedSupplierByWarehouseIdDueRequestHandler : IRequestHandler<GetSelectedSupplierByWarehouseIdDueRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;


        public GetSelectedSupplierByWarehouseIdDueRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSupplierByWarehouseIdDueRequest request, CancellationToken cancellationToken)
        {
            ICollection<Supplier> codeValues = await _SupplierRepository.FilterAsync(x => x.WarehouseId==request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SupplierName+"-"+ x.PhoneNo+"-"+ x.Address,
                Value = x.SupplierId
            }).ToList();
            return selectModels;
        }
    }
}
