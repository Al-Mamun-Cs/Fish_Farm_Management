using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.DepositorInstallments;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Queries
{
    public class GetDepositorInstallmentDetailRequestHandler : IRequestHandler<GetDepositorInstallmentDetailRequest, DepositorInstallmentDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<DepositorInstallment> _DepositorInstallmentRepository;
        public GetDepositorInstallmentDetailRequestHandler(ISchoolManagementRepository<DepositorInstallment> DepositorInstallmentRepository, IMapper mapper)
        {
            _DepositorInstallmentRepository = DepositorInstallmentRepository;
            _mapper = mapper;
        }
        public async Task<DepositorInstallmentDto> Handle(GetDepositorInstallmentDetailRequest request, CancellationToken cancellationToken)
        {
            var DepositorInstallment = await _DepositorInstallmentRepository.Get(request.DepositorInstallmentId);
            return _mapper.Map<DepositorInstallmentDto>(DepositorInstallment);
        }
    }
}
