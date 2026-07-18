using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Banks;
using SchoolManagement.Application.Features.Banks.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Banks.Handlers.Queries
{
    public class GetBankDetailRequestHandler : IRequestHandler<GetBankDetailRequest, BankDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<Bank> _BankRepository;
        public GetBankDetailRequestHandler(ISchoolManagementRepository<Bank> BankRepository, IMapper mapper)
        {
            _BankRepository = BankRepository;
            _mapper = mapper;
        }
        public async Task<BankDto> Handle(GetBankDetailRequest request, CancellationToken cancellationToken)
        {
            var Bank = await _BankRepository.Get(request.BankId);
            return _mapper.Map<BankDto>(Bank);
        }
    }
}
