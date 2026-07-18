using MediatR;

namespace SchoolManagement.Application.Features.Fiscalyears.Requests.Commands
{
    public class DeleteFiscalyearCommand : IRequest
    {
        public int FiscalyearId { get; set; }
    }
}
