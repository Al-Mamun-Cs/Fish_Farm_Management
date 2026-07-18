using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using SchoolManagement.Domain;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Queries
{
    public class SpGetFisheriesInventoryVoucherByIdRequestHandler : IRequestHandler<SpGetFisheriesInventoryVoucherByIdRequest, object>
    {

        private readonly ISchoolManagementRepository<FisheriesInventory> _FisheriesInventoryRepository;

        private readonly IMapper _mapper;

        public SpGetFisheriesInventoryVoucherByIdRequestHandler(ISchoolManagementRepository<FisheriesInventory> FisheriesInventoryRepository, IMapper mapper)
        {
            _FisheriesInventoryRepository = FisheriesInventoryRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(SpGetFisheriesInventoryVoucherByIdRequest request, CancellationToken cancellationToken)
        {
            var spQuery = String.Format("exec [SpGetFisheriesInventoryVoucherById] {0}", request.FisheriesInventoryId);

            DataTable dataTable = _FisheriesInventoryRepository.ExecWithSqlQuery(spQuery);

            return dataTable;


        }
    }
}
