using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class SpGetTotalCustomerDueAmountRequestHandler : IRequestHandler<SpGetTotalCustomerDueAmountRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;

        private readonly IMapper _mapper;

        public SpGetTotalCustomerDueAmountRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository, IMapper mapper)
        {
            _SupplierRepository = SupplierRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalCustomerDueAmountRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalCustomerDueAmount] {0}", request.WarehouseId);

            DataTable dataTable = _SupplierRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
