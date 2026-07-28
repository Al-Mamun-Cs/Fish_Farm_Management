using SchoolManagement.Application.Features.Ponds.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.Ponds.Handlers.Queries
{
    public class SpGetTotalFisheriesProductTypewiseCosetRequestHandler : IRequestHandler<SpGetTotalFisheriesProductTypewiseCosetRequest, DataSet>
    {

        private readonly ISchoolManagementRepository<Pond> _PondRepository;

        private readonly IMapper _mapper;

        public SpGetTotalFisheriesProductTypewiseCosetRequestHandler(ISchoolManagementRepository<Pond> PondRepository, IMapper mapper)
        {
            _PondRepository = PondRepository;
            _mapper = mapper;
        }

        public async Task<DataSet> Handle(SpGetTotalFisheriesProductTypewiseCosetRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalFisheriesProductTypewiseCoset] {0},{1}", request.WarehouseId,request.PondId);

            DataSet ds = _PondRepository.ExecWithSqlQueryDataSet(spQuery);

            return ds;


        }
    }
}
