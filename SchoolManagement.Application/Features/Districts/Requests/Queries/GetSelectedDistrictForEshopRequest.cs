using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Districts.Requests.Queries
{
    public class GetSelectedDistrictForEshopRequest : IRequest<List<SelectedModel>>
    {
        //public int DivisionId { get; set; }
    }
}
