using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Upozilas.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Upozilas.Handlers.Queries
{
    public class GetSelectedUpozilaRequestHandler : IRequestHandler<GetSelectedUpozilaRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Upozila> _UpozilaRepository;


        public GetSelectedUpozilaRequestHandler(ISchoolManagementRepository<Upozila> UpozilaRepository)
        {
            _UpozilaRepository = UpozilaRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedUpozilaRequest request, CancellationToken cancellationToken)
        {
            ICollection<Upozila> codeValues = await _UpozilaRepository.FilterAsync(x =>x.DistrictId==request.DistrictId && x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.UpazilaName,
                Value = x.UpazilaId
            }).ToList();
            return selectModels;
        }
    }
}
