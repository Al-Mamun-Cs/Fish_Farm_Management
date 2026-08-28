using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Queries
{
    public class SpGetDailyFishFeedConsumptionRequestHandler : IRequestHandler<SpGetDailyFishFeedConsumptionRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<FisheriesInventoryOut> _FisheriesInventoryOutRepository;

        private readonly IMapper _mapper;

        public SpGetDailyFishFeedConsumptionRequestHandler(ISchoolManagementRepository<FisheriesInventoryOut> FisheriesInventoryOutRepository, IMapper mapper)
        {
            _FisheriesInventoryOutRepository = FisheriesInventoryOutRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetDailyFishFeedConsumptionRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetDailyFishFeedConsumption] {0}", request.ProjectScheduleId);

            DataTable dataTable = _FisheriesInventoryOutRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
