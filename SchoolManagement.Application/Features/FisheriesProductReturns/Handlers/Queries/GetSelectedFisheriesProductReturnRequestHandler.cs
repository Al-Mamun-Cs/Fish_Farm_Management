using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Queries
{
    public class GetSelectedFisheriesProductReturnRequestHandler : IRequestHandler<GetSelectedFisheriesProductReturnRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FisheriesProductReturn> _FisheriesProductReturnRepository;


        public GetSelectedFisheriesProductReturnRequestHandler(ISchoolManagementRepository<FisheriesProductReturn> FisheriesProductReturnRepository)
        {
            _FisheriesProductReturnRepository = FisheriesProductReturnRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFisheriesProductReturnRequest request, CancellationToken cancellationToken)
        {
            ICollection<FisheriesProductReturn> codeValues = await _FisheriesProductReturnRepository.FilterAsync(x => x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ReturnQty,
                Value = x.FisheriesProductReturnId
            }).ToList();
            return selectModels;
        }
    }
}
