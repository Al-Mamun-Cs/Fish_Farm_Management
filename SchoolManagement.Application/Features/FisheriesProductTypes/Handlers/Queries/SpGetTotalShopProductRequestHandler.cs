using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class SpGetTotalShopProductRequestHandler : IRequestHandler<SpGetTotalShopProductRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;

        private readonly IMapper _mapper;

        public SpGetTotalShopProductRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository, IMapper mapper)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalShopProductRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalShopProduct] {0}", request.WarehouseId);

            DataTable dataTable = _FisheriesProductTypeRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
