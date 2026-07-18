using MediatR;
using SchoolManagement.Application.DTOs.Fiscalyears;

namespace SchoolManagement.Application.Features.Fiscalyears.Requests.Commands
{
    public class UpdateFiscalyearCommand : IRequest<Unit>
    {
        public FiscalyearDto FiscalyearDto { get; set; }
    }
}
