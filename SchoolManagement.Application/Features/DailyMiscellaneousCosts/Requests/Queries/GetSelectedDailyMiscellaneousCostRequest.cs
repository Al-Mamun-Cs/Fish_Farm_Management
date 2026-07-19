using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries
{
    public class GetSelectedDailyMiscellaneousCostRequest : IRequest<List<SelectedModel>>
    {
    }
}
