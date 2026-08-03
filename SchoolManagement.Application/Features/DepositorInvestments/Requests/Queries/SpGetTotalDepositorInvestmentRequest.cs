using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries
{
    public class SpGetTotalDepositorInvestmentRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
