using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Districts.Requests.Queries
{
    public class GetAutoCompleteDistrictRequest : IRequest<List<SelectedModel>>
    {
        public string DistrictName { get; set; }
    }
}
