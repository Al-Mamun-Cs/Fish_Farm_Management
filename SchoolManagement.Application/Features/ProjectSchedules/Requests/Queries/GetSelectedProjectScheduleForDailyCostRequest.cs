using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries
{
    public class GetSelectedProjectScheduleForDailyCostRequest : IRequest<List<SelectedModel>>
    {
        //public int? ProjectTypeId { get; set; }
    }
}
