using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class SpGetTotalDepositAmountRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
