using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Handlers.Queries
{
    public class GetSelectedFisheriesProductTypeRequestHandler : IRequestHandler<GetSelectedFisheriesProductTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<FisheriesProductType> _FisheriesProductTypeRepository;


        public GetSelectedFisheriesProductTypeRequestHandler(ISchoolManagementRepository<FisheriesProductType> FisheriesProductTypeRepository)
        {
            _FisheriesProductTypeRepository = FisheriesProductTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFisheriesProductTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<FisheriesProductType> codeValues = await _FisheriesProductTypeRepository.FilterAsync(x =>x.WarehouseId == request.WarehouseId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.NameBangla,
                Value = x.FisheriesProductTypeId
            }).ToList();
            return selectModels;
        }
    }
}
