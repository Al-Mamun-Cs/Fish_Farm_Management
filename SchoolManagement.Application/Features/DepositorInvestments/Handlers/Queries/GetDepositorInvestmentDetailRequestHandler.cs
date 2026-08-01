using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DepositorInvestments;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Queries
{
    public class GetDepositorInvestmentDetailRequestHandler : IRequestHandler<GetDepositorInvestmentDetailRequest, DepositorInvestmentDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DepositorInvestment> _DepositorInvestmentRepository;
        public GetDepositorInvestmentDetailRequestHandler(ISchoolManagementRepository<DepositorInvestment> DepositorInvestmentRepository, IMapper mapper)
        {
            _DepositorInvestmentRepository = DepositorInvestmentRepository;
            _mapper = mapper;
        }
        public async Task<DepositorInvestmentDto> Handle(GetDepositorInvestmentDetailRequest request, CancellationToken cancellationToken)
        {
            var DepositorInvestment = await _DepositorInvestmentRepository.Get(request.DepositorInvestmentId);
            return _mapper.Map<DepositorInvestmentDto>(DepositorInvestment);
        }
    }
}
