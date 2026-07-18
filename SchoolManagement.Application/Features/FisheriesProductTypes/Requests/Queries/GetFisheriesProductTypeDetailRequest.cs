using MediatR;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries
{
    public class GetFisheriesProductTypeDetailRequest : IRequest<FisheriesProductTypeDto>
    {
        public int FisheriesProductTypeId { get; set; }
    }
}
