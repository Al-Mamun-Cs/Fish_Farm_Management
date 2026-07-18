using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries
{
    public class GetSelectedFisheriesInventoryOutRequest : IRequest<List<SelectedModel>>
    {
    }
}
