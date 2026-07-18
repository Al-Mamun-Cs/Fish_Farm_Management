using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Genders;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Genders.Requests.Queries
{
    public class GetGenderListRequest : IRequest<PagedResult<GenderDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
