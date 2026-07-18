using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries
{
    public class SpGetTotalFisheriesProductTypeListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }

    }
}
