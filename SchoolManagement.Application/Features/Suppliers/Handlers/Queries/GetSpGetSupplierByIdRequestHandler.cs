using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetSpGetSupplierByIdRequestHandler : IRequestHandler<GetSpGetSupplierByIdRequest, object>
    {

        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository;

        private readonly IMapper _mapper;

        public GetSpGetSupplierByIdRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository, IMapper mapper)
        {
            _SupplierRepository = SupplierRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetSpGetSupplierByIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetSupplierDetailById] {0}", request.SupplierId);

            DataTable dataTable = _SupplierRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
