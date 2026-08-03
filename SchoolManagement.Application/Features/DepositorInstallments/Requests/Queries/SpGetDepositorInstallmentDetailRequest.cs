using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class SpGetDepositorInstallmentDetailRequest : IRequest<DataTable>
    {
        public int? DepositorId { get; set; }

    }
}
