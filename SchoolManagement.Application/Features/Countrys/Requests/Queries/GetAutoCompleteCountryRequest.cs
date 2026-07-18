using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Countrys.Requests.Queries
{
    public class GetAutoCompleteCountryRequest : IRequest<List<SelectedModel>>
    {
        public string Name { get; set; }
    }
}
