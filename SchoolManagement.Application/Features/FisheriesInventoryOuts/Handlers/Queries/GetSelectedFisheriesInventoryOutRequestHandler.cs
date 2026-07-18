using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Queries
{
    public class GetSelectedFisheriesInventoryOutRequestHandler : IRequestHandler<GetSelectedFisheriesInventoryOutRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FisheriesInventoryOut> _FisheriesInventoryOutRepository;


        public GetSelectedFisheriesInventoryOutRequestHandler(ISchoolManagementRepository<FisheriesInventoryOut> FisheriesInventoryOutRepository)
        {
            _FisheriesInventoryOutRepository = FisheriesInventoryOutRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFisheriesInventoryOutRequest request, CancellationToken cancellationToken)
        {
            ICollection<FisheriesInventoryOut> codeValues = await _FisheriesInventoryOutRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Date,
                Value = x.FisheriesInventoryOutId
            }).ToList();
            return selectModels;
        }
    }
}
