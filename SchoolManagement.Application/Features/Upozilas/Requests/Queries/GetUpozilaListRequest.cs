using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Upozilas;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Queries
{
    public class GetUpozilaListRequest : IRequest<PagedResult<UpozilaDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
