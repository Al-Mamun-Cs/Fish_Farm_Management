using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Queries
{
    public class GetSelectedUpozilaRequest : IRequest<List<SelectedModel>>
    {
        public int DistrictId { get; set; }
    }
}
