using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries
{
    public class GetSelectedEasyBikeBankNamesRequest : IRequest<List<SelectedModel>>
    {
    }
}
