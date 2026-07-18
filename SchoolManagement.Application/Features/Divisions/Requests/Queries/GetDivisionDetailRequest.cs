using MediatR;
using SchoolManagement.Application.DTOs.Divisions;

namespace SchoolManagement.Application.Features.Divisions.Requests.Queries
{
    public class GetDivisionDetailRequest : IRequest<DivisionDto>
    {
        public int DivisionId { get; set; }
    }
}
