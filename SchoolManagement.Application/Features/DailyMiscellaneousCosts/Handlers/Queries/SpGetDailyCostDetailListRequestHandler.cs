using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Queries
{
    public class SpGetDailyCostDetailListRequestHandler : IRequestHandler<SpGetDailyCostDetailListRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<DailyMiscellaneousCost> _DailyMiscellaneousCostRepository;

        private readonly IMapper _mapper;

        public SpGetDailyCostDetailListRequestHandler(ISchoolManagementRepository<DailyMiscellaneousCost> DailyMiscellaneousCostRepository, IMapper mapper)
        {
            _DailyMiscellaneousCostRepository = DailyMiscellaneousCostRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetDailyCostDetailListRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetDailyCostDetailList] {0}", request.WarehouseId);

            DataTable dataTable = _DailyMiscellaneousCostRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
