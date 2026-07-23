using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Queries
{
    public class SpGetDailyAssetCostDetailListRequestHandler : IRequestHandler<SpGetDailyAssetCostDetailListRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<DailyMiscellaneousCost> _DailyMiscellaneousCostRepository;

        private readonly IMapper _mapper;

        public SpGetDailyAssetCostDetailListRequestHandler(ISchoolManagementRepository<DailyMiscellaneousCost> DailyMiscellaneousCostRepository, IMapper mapper)
        {
            _DailyMiscellaneousCostRepository = DailyMiscellaneousCostRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetDailyAssetCostDetailListRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetDailyAssetCostDetailList] {0}", request.WarehouseId);

            DataTable dataTable = _DailyMiscellaneousCostRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
