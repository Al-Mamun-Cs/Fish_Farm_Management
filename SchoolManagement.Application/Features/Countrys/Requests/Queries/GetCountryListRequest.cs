using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Countrys;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Countrys.Requests.Queries
{
    public class GetCountryListRequest : IRequest<PagedResult<CountryDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
