using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class SpGetShopProductStockByIdRequestHandler : IRequestHandler<SpGetShopProductStockByIdRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;

        private readonly IMapper _mapper;

        public SpGetShopProductStockByIdRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository, IMapper mapper)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetShopProductStockByIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetShopProductStockById] {0},{1}", request.WarehouseId,request.FisheriesProductTypeId);

            DataTable dataTable = _FisheriesProductTypeRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
