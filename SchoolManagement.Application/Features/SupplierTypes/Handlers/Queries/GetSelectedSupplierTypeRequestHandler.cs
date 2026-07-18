using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SupplierTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SupplierTypes.Handlers.Queries
{
    public class GetSelectedSupplierTypeRequestHandler : IRequestHandler<GetSelectedSupplierTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SupplierType> _SupplierTypeRepository;


        public GetSelectedSupplierTypeRequestHandler(ISchoolManagementRepository<SupplierType> SupplierTypeRepository)
        {
            _SupplierTypeRepository = SupplierTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSupplierTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<SupplierType> codeValues = await _SupplierTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SupplierTypeName,
                Value = x.SupplierTypeId
            }).ToList();
            return selectModels;
        }
    }
}
