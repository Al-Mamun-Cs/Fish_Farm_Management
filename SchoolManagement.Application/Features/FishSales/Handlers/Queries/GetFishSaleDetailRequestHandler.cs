using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.FishSales;
using SchoolManagement.Application.Features.FishSales.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FishSales.Handlers.Queries
{
    public class GetFishSaleDetailRequestHandler : IRequestHandler<GetFishSaleDetailRequest, FishSaleDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<FishSale> _FishSaleRepository;
        public GetFishSaleDetailRequestHandler(ISchoolManagementRepository<FishSale> FishSaleRepository, IMapper mapper)
        {
            _FishSaleRepository = FishSaleRepository;
            _mapper = mapper;
        }
        public async Task<FishSaleDto> Handle(GetFishSaleDetailRequest request, CancellationToken cancellationToken)
        {
            var FishSale = await _FishSaleRepository.Get(request.FishSaleId);
            return _mapper.Map<FishSaleDto>(FishSale);
        }
    }
}
