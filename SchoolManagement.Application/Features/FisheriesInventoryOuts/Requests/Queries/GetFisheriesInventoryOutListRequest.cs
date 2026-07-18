using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries
{
    public class GetFisheriesInventoryOutListRequest : IRequest<PagedResult<FisheriesInventoryOutDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
