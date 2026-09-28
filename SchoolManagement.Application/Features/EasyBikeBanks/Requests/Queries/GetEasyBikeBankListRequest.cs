using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries
{
    public class GetEasyBikeBankListRequest : IRequest<PagedResult<EasyBikeBankDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
