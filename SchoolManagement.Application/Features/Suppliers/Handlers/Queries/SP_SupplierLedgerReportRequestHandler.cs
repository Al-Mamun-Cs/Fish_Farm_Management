using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using System.Data;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class SP_SupplierLedgerReportRequestHandler : IRequestHandler<SP_SupplierLedgerReportRequest, object>
    {

        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;

        private readonly IMapper _mapper;

        public SP_SupplierLedgerReportRequestHandler(ISchoolManagementRepository<Supplier> FlyingTimeByAricraftRepository, IMapper mapper)
        {
            _SupplierRepository = FlyingTimeByAricraftRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SP_SupplierLedgerReportRequest request, CancellationToken cancellationToken)
        {
           // object obj = new object();
            var spQuery = String.Format("exec [SP_SupplierLedgerReport] {0}, '{1}','{2}'", request.SupplierId, request.DateFrom, request.DateTo );

            DataTable dataTable = _SupplierRepository.ExecWithSqlQuery(spQuery);
           
            return dataTable;
         
        }
    }
}
