using SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Queries
{
    public class SpGetDepstInvestmentDetailRequestHandler : IRequestHandler<SpGetDepstInvestmentDetailRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<DepositorInvestment> _DepositorInvestmentRepository;

        private readonly IMapper _mapper;

        public SpGetDepstInvestmentDetailRequestHandler(ISchoolManagementRepository<DepositorInvestment> DepositorInvestmentRepository, IMapper mapper)
        {
            _DepositorInvestmentRepository = DepositorInvestmentRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetDepstInvestmentDetailRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetDepstInvestmentDetail] {0}", request.WarehouseId);

            DataTable dataTable = _DepositorInvestmentRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
