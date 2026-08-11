using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Queries
{
    public class GetFisheriesProductReturnDetailRequestHandler : IRequestHandler<GetFisheriesProductReturnDetailRequest, FisheriesProductReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FisheriesProductReturn> _FisheriesProductReturnRepository;
        public GetFisheriesProductReturnDetailRequestHandler(ISchoolManagementRepository<FisheriesProductReturn> FisheriesProductReturnRepository, IMapper mapper)
        {
            _FisheriesProductReturnRepository = FisheriesProductReturnRepository;
            _mapper = mapper;
        }
        public async Task<FisheriesProductReturnDto> Handle(GetFisheriesProductReturnDetailRequest request, CancellationToken cancellationToken)
        {
            var FisheriesProductReturn = await _FisheriesProductReturnRepository.Get(request.FisheriesProductReturnId);
            return _mapper.Map<FisheriesProductReturnDto>(FisheriesProductReturn);
        }
    }
}
