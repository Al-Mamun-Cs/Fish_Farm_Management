using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Designations.Requests.Queries
{
    public class GetSelectedDesignationRequest : IRequest<List<SelectedModel>>
    {
        public int WarehouseId { get; set; }
    }
}
