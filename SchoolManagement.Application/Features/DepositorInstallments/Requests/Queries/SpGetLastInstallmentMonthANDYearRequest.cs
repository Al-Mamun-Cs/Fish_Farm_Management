using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class SpGetLastInstallmentMonthANDYearRequest : IRequest<DataTable>
    {
        public int? DepositorId { get; set; }

    }
}
