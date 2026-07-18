using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class SpGetFisheriesProductStockByIdListRequestHandler : IRequestHandler<SpGetFisheriesProductStockByIdListRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;

        private readonly IMapper _mapper;

        public SpGetFisheriesProductStockByIdListRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository, IMapper mapper)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetFisheriesProductStockByIdListRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetFisheriesProductStockById] {0},{1}", request.WarehouseId,request.FisheriesProductTypeId);

            DataTable dataTable = _FisheriesProductTypeRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
