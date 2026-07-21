using SchoolManagement.Application.Features.Warehouses.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.Warehouses.Handlers.Queries
{
    public class SpGetTotalCashCapitalRequestHandler : IRequestHandler<SpGetTotalCashCapitalRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<Warehouse> _WarehouseRepository;

        private readonly IMapper _mapper;

        public SpGetTotalCashCapitalRequestHandler(ISchoolManagementRepository<Warehouse> WarehouseRepository, IMapper mapper)
        {
            _WarehouseRepository = WarehouseRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalCashCapitalRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalCashCapital] {0}", request.WarehouseId);

            DataTable dataTable = _WarehouseRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
