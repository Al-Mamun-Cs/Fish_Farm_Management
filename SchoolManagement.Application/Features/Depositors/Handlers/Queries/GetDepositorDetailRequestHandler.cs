using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Depositors;
using SchoolManagement.Application.Features.Depositors.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Depositors.Handlers.Queries
{
    public class GetDepositorDetailRequestHandler : IRequestHandler<GetDepositorDetailRequest, DepositorDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Depositor> _DepositorRepository;
        public GetDepositorDetailRequestHandler(ISchoolManagementRepository<Depositor> DepositorRepository, IMapper mapper)
        {
            _DepositorRepository = DepositorRepository;
            _mapper = mapper;
        }
        public async Task<DepositorDto> Handle(GetDepositorDetailRequest request, CancellationToken cancellationToken)
        {
            var Depositor = await _DepositorRepository.Get(request.DepositorId);
            return _mapper.Map<DepositorDto>(Depositor);
        }
    }
}
