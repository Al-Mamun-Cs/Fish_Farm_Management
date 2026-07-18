using MediatR;
using System.Data;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries
{
    public class SpGetFisheriesProductStockByIdListRequest : IRequest<DataTable>
    {
        public int? WarehouseId { get; set; }
        public int? FisheriesProductTypeId { get; set; }

    }
}
