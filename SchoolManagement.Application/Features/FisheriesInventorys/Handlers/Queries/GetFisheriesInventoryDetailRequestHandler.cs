using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Queries
{
    public class GetFisheriesInventoryDetailRequestHandler : IRequestHandler<GetFisheriesInventoryDetailRequest, FisheriesInventoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FisheriesInventory> _FisheriesInventoryRepository;
        public GetFisheriesInventoryDetailRequestHandler(ISchoolManagementRepository<FisheriesInventory> FisheriesInventoryRepository, IMapper mapper)
        {
            _FisheriesInventoryRepository = FisheriesInventoryRepository;
            _mapper = mapper;
        }
        

        public async Task<FisheriesInventoryDto> Handle(GetFisheriesInventoryDetailRequest request, CancellationToken cancellationToken)
        {
            var FisheriesInventory = await _FisheriesInventoryRepository.FindOneAsync(
            x => x.FisheriesInventoryId == request.FisheriesInventoryId);


            return _mapper.Map<FisheriesInventoryDto>(FisheriesInventory);
        }
    }
}
