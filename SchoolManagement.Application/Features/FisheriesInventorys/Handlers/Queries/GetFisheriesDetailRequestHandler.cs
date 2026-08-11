using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Queries
{
    public class GetFisheriesDetailRequestHandler : IRequestHandler<GetFisheriesDetailRequest, FisheriesInventoryDetailDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Domain.FisheriesInventoryDetail> _FisheriesInventoryRepository;
        public GetFisheriesDetailRequestHandler(ISchoolManagementRepository<Domain.FisheriesInventoryDetail> FisheriesInventoryRepository, IMapper mapper)
        {
            _FisheriesInventoryRepository = FisheriesInventoryRepository;
            _mapper = mapper;
        }
        

        public async Task<FisheriesInventoryDetailDto> Handle(GetFisheriesDetailRequest request, CancellationToken cancellationToken)
        {
            var FisheriesInventory = await _FisheriesInventoryRepository.FindOneAsync(
            x => x.FisheriesInventoryDetailId == request.FisheriesInventoryDetailId);


            return _mapper.Map<FisheriesInventoryDetailDto>(FisheriesInventory);
        }
    }
}
