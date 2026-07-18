using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class SpGetTotalFisheriesProductTypeListRequestHandler : IRequestHandler<SpGetTotalFisheriesProductTypeListRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;

        private readonly IMapper _mapper;

        public SpGetTotalFisheriesProductTypeListRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository, IMapper mapper)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalFisheriesProductTypeListRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalFisheriesProductTypeList] {0}",request.WarehouseId);

            DataTable dataTable = _FisheriesProductTypeRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
