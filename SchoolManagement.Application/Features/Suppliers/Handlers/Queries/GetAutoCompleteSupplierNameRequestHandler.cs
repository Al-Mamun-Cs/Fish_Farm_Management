using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetAutoCompleteSupplierNameRequestHandler : IRequestHandler<GetAutoCompleteSupplierNameRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;
        public GetAutoCompleteSupplierNameRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetAutoCompleteSupplierNameRequest request, CancellationToken cancellationToken)
        {
            ICollection<Supplier> traineeBioDataGeneralInfos = await _SupplierRepository.FilterAsync(x => x.SupplierStatus == 1 && x.SupplierName.Contains(request.SupplierName));
            var selectModels = traineeBioDataGeneralInfos.Select(x => new SelectedModel
            {
                Text = x.SupplierName + " - " + x.PhoneNo,
                Value = x.SupplierId
            }).ToList();
            return selectModels;
        }
    }
}
