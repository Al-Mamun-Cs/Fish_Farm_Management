using SchoolManagement.Application.Features.ShopInventorys.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Queries
{
    public class SpGetShopInventoryVoucherByIdRequestHandler : IRequestHandler<SpGetShopInventoryVoucherByIdRequest, object>
    {

        private readonly ISchoolManagementRepository<ShopInventory> _ShopInventoryRepository;

        private readonly IMapper _mapper;

        public SpGetShopInventoryVoucherByIdRequestHandler(ISchoolManagementRepository<ShopInventory> ShopInventoryRepository, IMapper mapper)
        {
            _ShopInventoryRepository = ShopInventoryRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SpGetShopInventoryVoucherByIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetShopInventoryVoucherById] {0}", request.ShopInventoryId);

            DataTable dataTable = _ShopInventoryRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
