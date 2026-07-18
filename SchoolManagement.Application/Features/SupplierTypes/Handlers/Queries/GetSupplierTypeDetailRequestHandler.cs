using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SupplierTypes;
using SchoolManagement.Application.Features.SupplierTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SupplierTypes.Handlers.Queries
{
    public class GetSupplierTypeDetailRequestHandler : IRequestHandler<GetSupplierTypeDetailRequest, SupplierTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SupplierType> _SupplierTypeRepository;
        public GetSupplierTypeDetailRequestHandler(ISchoolManagementRepository<SupplierType> SupplierTypeRepository, IMapper mapper)
        {
            _SupplierTypeRepository = SupplierTypeRepository;
            _mapper = mapper;
        }
        public async Task<SupplierTypeDto> Handle(GetSupplierTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var SupplierType = await _SupplierTypeRepository.Get(request.SupplierTypeId);
            return _mapper.Map<SupplierTypeDto>(SupplierType);
        }
    }
}
