using SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Queries
{
    public class SpGetTotalInvestorRequestHandler : IRequestHandler<SpGetTotalInvestorRequest, DataTable>
    {

        private readonly ISchoolManagementRepository<CompanyInvestor> _CompanyInvestorRepository;

        private readonly IMapper _mapper;

        public SpGetTotalInvestorRequestHandler(ISchoolManagementRepository<CompanyInvestor> CompanyInvestorRepository, IMapper mapper)
        {
            _CompanyInvestorRepository = CompanyInvestorRepository;
            _mapper = mapper;
        }

        public async Task<DataTable> Handle(SpGetTotalInvestorRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetTotalInvestor] {0}", request.WarehouseId);

            DataTable dataTable = _CompanyInvestorRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
