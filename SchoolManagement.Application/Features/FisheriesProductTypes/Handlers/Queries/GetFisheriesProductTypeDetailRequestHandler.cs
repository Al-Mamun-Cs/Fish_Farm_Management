using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class GetFisheriesProductTypeDetailRequestHandler : IRequestHandler<GetFisheriesProductTypeDetailRequest, FisheriesProductTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;
        public GetFisheriesProductTypeDetailRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository, IMapper mapper)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
            _mapper = mapper;
        }
        public async Task<FisheriesProductTypeDto> Handle(GetFisheriesProductTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var FisheriesProductType = await _FisheriesProductTypeRepository.Get(request.FisheriesProductTypeId);
            return _mapper.Map<FisheriesProductTypeDto>(FisheriesProductType);
        }
    }
}
