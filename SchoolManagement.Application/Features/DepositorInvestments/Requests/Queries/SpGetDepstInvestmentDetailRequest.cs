using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries
{
    public class SpGetDepstInvestmentDetailRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
