using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries
{
    public class GetSelectedFisheriesProductTypeRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
