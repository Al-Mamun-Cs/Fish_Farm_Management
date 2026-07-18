using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Queries
{
    public class GetFisheriesInventoryOutDetailRequestHandler : IRequestHandler<GetFisheriesInventoryOutDetailRequest, FisheriesInventoryOutDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FisheriesInventoryOut> _FisheriesInventoryOutRepository;
        public GetFisheriesInventoryOutDetailRequestHandler(ISchoolManagementRepository<FisheriesInventoryOut> FisheriesInventoryOutRepository, IMapper mapper)
        {
            _FisheriesInventoryOutRepository = FisheriesInventoryOutRepository;
            _mapper = mapper;
        }
        public async Task<FisheriesInventoryOutDto> Handle(GetFisheriesInventoryOutDetailRequest request, CancellationToken cancellationToken)
        {
            var FisheriesInventoryOut = await _FisheriesInventoryOutRepository.Get(request.FisheriesInventoryOutId);
            return _mapper.Map<FisheriesInventoryOutDto>(FisheriesInventoryOut);
        }
    }
}
