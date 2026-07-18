using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Banks.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Banks.Handlers.Queries
{
    public class GetSelectedBankRequestHandler : IRequestHandler<GetSelectedBankRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Bank> _BankRepository;


        public GetSelectedBankRequestHandler(ISchoolManagementRepository<Bank> BankRepository)
        {
            _BankRepository = BankRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBankRequest request, CancellationToken cancellationToken)
        {
            ICollection<Bank> codeValues = await _BankRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.BankName,
                Value = x.BankId
            }).ToList();
            return selectModels;
        }
    }
}
