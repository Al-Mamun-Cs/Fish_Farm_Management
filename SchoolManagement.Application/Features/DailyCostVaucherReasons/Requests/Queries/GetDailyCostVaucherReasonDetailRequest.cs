using MediatR;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;

namespace SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries
{
    public class GetDailyCostVaucherReasonDetailRequest : IRequest<DailyCostVaucherReasonDto>
    {
        public int DailyCostVaucherReasonId { get; set; }
    }
}
