using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.Ponds.Requests.Queries
{
    public class SpGetTotalFisheriesProductTypewiseCosetRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }
        public int? PondId { get; set; }

    }
}
