using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Banks;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Banks.Requests.Queries
{
    public class GetBankListRequest : IRequest<PagedResult<BankDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
