using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Queries
{
    public class SpGetFisheriesBillNoRequestHandler : IRequestHandler<SpGetFisheriesBillNoRequest, object>
    {

        private readonly ISchoolManagementRepository<FisheriesInventory> _FisheriesInventoryRepository;

        private readonly IMapper _mapper;

        public SpGetFisheriesBillNoRequestHandler(ISchoolManagementRepository<FisheriesInventory> FlyingTimeByAricraftRepository, IMapper mapper)
        {
            _FisheriesInventoryRepository = FlyingTimeByAricraftRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SpGetFisheriesBillNoRequest request, CancellationToken cancellationToken)
        {
            // object obj = new object();
            var spQuery = String.Format("exec [SpGetFisheriesBillNo]");

            DataTable dataTable = _FisheriesInventoryRepository.ExecWithSqlQuery(spQuery);

            return dataTable;

        }
    }
}
