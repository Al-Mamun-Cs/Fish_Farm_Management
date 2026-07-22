using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ShopHandCashWithdrows.Handlers.Queries
{
    public class GetSelectedShopHandCashWithdrowRequestHandler : IRequestHandler<GetSelectedShopHandCashWithdrowRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ShopHandCashWithdrow> _ShopHandCashWithdrowRepository;


        public GetSelectedShopHandCashWithdrowRequestHandler(ISchoolManagementRepository<ShopHandCashWithdrow> ShopHandCashWithdrowRepository)
        {
            _ShopHandCashWithdrowRepository = ShopHandCashWithdrowRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedShopHandCashWithdrowRequest request, CancellationToken cancellationToken)
        {
            ICollection<ShopHandCashWithdrow> codeValues = await _ShopHandCashWithdrowRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.TransferAmount,
                Value = x.ShopHandCashWithdrowId
            }).ToList();
            return selectModels;
        }
    }
}
