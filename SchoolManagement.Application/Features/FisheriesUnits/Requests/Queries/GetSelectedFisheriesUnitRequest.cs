using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries
{
    public class GetSelectedFisheriesUnitRequest : IRequest<List<SelectedModel>>
    {
    }
}
