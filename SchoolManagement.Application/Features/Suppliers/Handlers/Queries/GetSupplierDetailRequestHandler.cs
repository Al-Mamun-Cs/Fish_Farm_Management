using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetSupplierDetailRequestHandler : IRequestHandler<GetSupplierDetailRequest, SupplierDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;
        public GetSupplierDetailRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository, IMapper mapper)
        {
            _SupplierRepository = SupplierRepository;
            _mapper = mapper;
        }
        public async Task<SupplierDto> Handle(GetSupplierDetailRequest request, CancellationToken cancellationToken)
        {
            //var Supplier = await _SupplierRepository.Get(request.SupplierId);
            //return _mapper.Map<SupplierDto>(Supplier);
            var Supplier = _SupplierRepository.FinedOneInclude(x => x.SupplierId == request.SupplierId, "District", "Upozila");
            return _mapper.Map<SupplierDto>(Supplier);
        }
    }
}
