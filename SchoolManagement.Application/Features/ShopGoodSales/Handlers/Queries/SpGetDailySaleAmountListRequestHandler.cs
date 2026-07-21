using SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Queries
{
    public class SpGetDailySaleAmountListRequestHandler : IRequestHandler<SpGetDailySaleAmountListRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<ShopGoodSale> _ShopGoodSaleRepository;

        private readonly IMapper _mapper;

        public SpGetDailySaleAmountListRequestHandler(ISchoolManagementRepository<ShopGoodSale> ShopGoodSaleRepository, IMapper mapper)
        {
            _ShopGoodSaleRepository = ShopGoodSaleRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetDailySaleAmountListRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetDailySaleAmountList] {0}", request.WarehouseId);

            DataTable dataTable = _ShopGoodSaleRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
