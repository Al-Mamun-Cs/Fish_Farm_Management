using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Queries
{
    public class SpGetShopGoodSaleBillNoRequestHandler : IRequestHandler<SpGetShopGoodSaleBillNoRequest, object>
    {

        private readonly ISchoolManagementRepository<ShopGoodSale> _ShopGoodSaleRepository;

        private readonly IMapper _mapper;

        public SpGetShopGoodSaleBillNoRequestHandler(ISchoolManagementRepository<ShopGoodSale> FlyingTimeByAricraftRepository, IMapper mapper)
        {
            _ShopGoodSaleRepository = FlyingTimeByAricraftRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SpGetShopGoodSaleBillNoRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetShopGoodSaleBillNo]");

            DataTable dataTable = _ShopGoodSaleRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
