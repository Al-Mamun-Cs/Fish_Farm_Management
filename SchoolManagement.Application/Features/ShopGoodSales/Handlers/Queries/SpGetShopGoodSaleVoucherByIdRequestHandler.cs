using SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.ShopGoodSales.Handlers.Queries
{
    public class SpGetShopGoodSaleVoucherByIdRequestHandler : IRequestHandler<SpGetShopGoodSaleVoucherByIdRequest, object>
    {

        private readonly ISchoolManagementRepository<ShopGoodSale> _ShopGoodSaleRepository;

        private readonly IMapper _mapper;

        public SpGetShopGoodSaleVoucherByIdRequestHandler(ISchoolManagementRepository<ShopGoodSale> ShopGoodSaleRepository, IMapper mapper)
        {
            _ShopGoodSaleRepository = ShopGoodSaleRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SpGetShopGoodSaleVoucherByIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetShopGoodSaleVoucherById] {0}", request.ShopGoodSaleId);

            DataTable dataTable = _ShopGoodSaleRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
