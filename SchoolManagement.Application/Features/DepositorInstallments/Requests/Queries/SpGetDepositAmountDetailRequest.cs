using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries
{
    public class SpGetDepositAmountDetailRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
