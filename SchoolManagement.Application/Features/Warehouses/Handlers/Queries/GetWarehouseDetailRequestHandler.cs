using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Application.Features.Warehouses.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Queries
{
    public class GetWarehouseDetailRequestHandler : IRequestHandler<GetWarehouseDetailRequest, WarehouseDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Warehouse> _WarehouseRepository;
        public GetWarehouseDetailRequestHandler(ISchoolManagementRepository<Warehouse> WarehouseRepository, IMapper mapper)
        {
            _WarehouseRepository = WarehouseRepository;
            _mapper = mapper;
        }
        public async Task<WarehouseDto> Handle(GetWarehouseDetailRequest request, CancellationToken cancellationToken)
        {
            var Warehouse = await _WarehouseRepository.Get(request.WarehouseId);
            return _mapper.Map<WarehouseDto>(Warehouse);
        }
    }
}
