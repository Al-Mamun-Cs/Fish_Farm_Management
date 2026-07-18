using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.Ponds.Requests.Queries
{
    public class SpGetTotalFisheriesPondListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
