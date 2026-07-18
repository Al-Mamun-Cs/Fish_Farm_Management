using MediatR;
using SchoolManagement.Application.DTOs.BloodGroups;

namespace SchoolManagement.Application.Features.BloodGroups.Requests.Queries
{
    public class GetBloodGroupDetailRequest : IRequest<BloodGroupDto>
    {
        public int BloodGroupId { get; set; }
    }
}
