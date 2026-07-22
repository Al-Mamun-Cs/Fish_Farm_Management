using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Queries
{
    public class GetShopHandCashWithdrowDetailRequestHandler : IRequestHandler<GetShopHandCashWithdrowDetailRequest, ShopHandCashWithdrowDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<ShopHandCashWithdrow> _ShopHandCashWithdrowRepository;
        public GetShopHandCashWithdrowDetailRequestHandler(ISchoolManagementRepository<ShopHandCashWithdrow> ShopHandCashWithdrowRepository, IMapper mapper)
        {
            _ShopHandCashWithdrowRepository = ShopHandCashWithdrowRepository;
            _mapper = mapper;
        }
        public async Task<ShopHandCashWithdrowDto> Handle(GetShopHandCashWithdrowDetailRequest request, CancellationToken cancellationToken)
        {
            var ShopHandCashWithdrow = await _ShopHandCashWithdrowRepository.Get(request.ShopHandCashWithdrowId);
            return _mapper.Map<ShopHandCashWithdrowDto>(ShopHandCashWithdrow);
        }
    }
}
