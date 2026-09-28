using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Handlers.Queries
{
    public class GetSelectedEasyBikeBankRequestHandler : IRequestHandler<GetSelectedEasyBikeBankRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<EasyBikeBank> _EasyBikeBankRepository;


        public GetSelectedEasyBikeBankRequestHandler(ISchoolManagementRepository<EasyBikeBank> EasyBikeBankRepository)
        {
            _EasyBikeBankRepository = EasyBikeBankRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedEasyBikeBankRequest request, CancellationToken cancellationToken)
        {
            ICollection<EasyBikeBank> codeValues = await _EasyBikeBankRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.BankName + " - " + x.BankAccountNo + " B: " + x.BankBalance,
                Value = x.EasyBikeBankId
            }).ToList();
            return selectModels;
        }
    }
}
