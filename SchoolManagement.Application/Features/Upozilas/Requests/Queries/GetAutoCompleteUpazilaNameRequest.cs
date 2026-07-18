using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Queries
{
    public class GetAutoCompleteUpazilaNameRequest : IRequest<List<SelectedModel>>
    {
        public string UpazilaName { get; set; }
    }
}
