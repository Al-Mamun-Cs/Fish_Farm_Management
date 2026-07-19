using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class SpGetTotalSupplierDueAmountRequestHandler : IRequestHandler<SpGetTotalSupplierDueAmountRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;

        private readonly IMapper _mapper;

        public SpGetTotalSupplierDueAmountRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository, IMapper mapper)
        {
            _SupplierRepository = SupplierRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalSupplierDueAmountRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalSupplierDueAmount] {0}", request.WarehouseId);

            DataTable dataTable = _SupplierRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
