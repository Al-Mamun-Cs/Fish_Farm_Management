using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Designations.Requests.Queries
{
    public class GetSelectedDesignationForManagementRequest : IRequest<List<SelectedModel>>
    {
        
    }
}
