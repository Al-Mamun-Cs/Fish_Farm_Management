using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Countrys.Requests.Queries
{
    public class GetSelectedCountryRequest : IRequest<List<SelectedModel>>
    {
    }
}
