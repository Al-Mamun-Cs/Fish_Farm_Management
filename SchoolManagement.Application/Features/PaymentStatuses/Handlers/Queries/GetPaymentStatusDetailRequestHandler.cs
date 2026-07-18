using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PaymentStatuses;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PaymentStatuses.Handlers.Queries
{
    public class GetPaymentStatusDetailRequestHandler : IRequestHandler<GetPaymentStatusDetailRequest, PaymentStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<PaymentStatus> _PaymentStatusRepository;
        public GetPaymentStatusDetailRequestHandler(ISchoolManagementRepository<PaymentStatus> PaymentStatusRepository, IMapper mapper)
        {
            _PaymentStatusRepository = PaymentStatusRepository;
            _mapper = mapper;
        }
        public async Task<PaymentStatusDto> Handle(GetPaymentStatusDetailRequest request, CancellationToken cancellationToken)
        {
            var PaymentStatus = await _PaymentStatusRepository.Get(request.PaymentStatusId);
            return _mapper.Map<PaymentStatusDto>(PaymentStatus);
        }
    }
}
