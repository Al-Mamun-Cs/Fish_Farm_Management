using SchoolManagement.Application.Features.Ponds.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Queries
{
    public class SpGetTotalFisheriesPondListRequestHandler : IRequestHandler<SpGetTotalFisheriesPondListRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<Pond> _PondRepository;

        private readonly IMapper _mapper;

        public SpGetTotalFisheriesPondListRequestHandler(ISchoolManagementRepository<Pond> PondRepository, IMapper mapper)
        {
            _PondRepository = PondRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalFisheriesPondListRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalFisheriesPondList] {0}", request.WarehouseId);

            DataTable dataTable = _PondRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
