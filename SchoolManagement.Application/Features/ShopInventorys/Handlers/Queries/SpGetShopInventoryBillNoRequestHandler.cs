using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Queries;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.ShopInventorys.Handlers.Queries
{
    public class SpGetShopInventoryBillNoRequestHandler : IRequestHandler<SpGetShopInventoryBillNoRequest, object>
    {

        private readonly ISchoolManagementRepository<ShopInventory> _ShopInventoryRepository;

        private readonly IMapper _mapper;

        public SpGetShopInventoryBillNoRequestHandler(ISchoolManagementRepository<ShopInventory> FlyingTimeByAricraftRepository, IMapper mapper)
        {
            _ShopInventoryRepository = FlyingTimeByAricraftRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SpGetShopInventoryBillNoRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetShopInventoryBillNo]");

            DataTable dataTable = _ShopInventoryRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
