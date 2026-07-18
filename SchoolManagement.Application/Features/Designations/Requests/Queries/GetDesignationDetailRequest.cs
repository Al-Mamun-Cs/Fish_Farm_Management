using MediatR;
using SchoolManagement.Application.DTOs.Designations;

namespace SchoolManagement.Application.Features.Designations.Requests.Queries
{
    public class GetDesignationDetailRequest : IRequest<DesignationDto>
    {
        public int DesignationId { get; set; }
    }
}
