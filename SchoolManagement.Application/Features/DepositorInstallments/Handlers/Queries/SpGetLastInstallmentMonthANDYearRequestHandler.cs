using SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInstallments.Handlers.Queries
{
    public class SpGetLastInstallmentMonthANDYearRequestHandler : IRequestHandler<SpGetLastInstallmentMonthANDYearRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<DepositorInstallment> _DepositorInstallmentRepository;

        private readonly IMapper _mapper;

        public SpGetLastInstallmentMonthANDYearRequestHandler(ISchoolManagementRepository<DepositorInstallment> DepositorInstallmentRepository, IMapper mapper)
        {
            _DepositorInstallmentRepository = DepositorInstallmentRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetLastInstallmentMonthANDYearRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetLastInstallmentMonthANDYear] {0}", request.DepositorId);

            DataTable dataTable = _DepositorInstallmentRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
