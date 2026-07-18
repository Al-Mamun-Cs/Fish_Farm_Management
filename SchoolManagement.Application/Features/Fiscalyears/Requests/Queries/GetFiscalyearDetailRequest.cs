using MediatR;
using SchoolManagement.Application.DTOs.Fiscalyears;

namespace SchoolManagement.Application.Features.Fiscalyears.Requests.Queries
{
    public class GetFiscalyearDetailRequest : IRequest<FiscalyearDto>
    {
        public int FiscalyearId { get; set; }
    }
}
