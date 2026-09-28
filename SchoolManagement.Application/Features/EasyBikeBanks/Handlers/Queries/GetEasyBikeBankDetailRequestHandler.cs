using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Handlers.Queries
{
    public class GetEasyBikeBankDetailRequestHandler : IRequestHandler<GetEasyBikeBankDetailRequest, EasyBikeBankDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<EasyBikeBank> _EasyBikeBankRepository;
        public GetEasyBikeBankDetailRequestHandler(ISchoolManagementRepository<EasyBikeBank> EasyBikeBankRepository, IMapper mapper)
        {
            _EasyBikeBankRepository = EasyBikeBankRepository;
            _mapper = mapper;
        }
        public async Task<EasyBikeBankDto> Handle(GetEasyBikeBankDetailRequest request, CancellationToken cancellationToken)
        {
            var EasyBikeBank = await _EasyBikeBankRepository.Get(request.EasyBikeBankId);
            return _mapper.Map<EasyBikeBankDto>(EasyBikeBank);
        }
    }
}
