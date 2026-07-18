using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Banks.Requests.Queries
{
    public class GetSelectedBankRequest : IRequest<List<SelectedModel>>
    {
    }
}
