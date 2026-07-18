using MediatR;

namespace SchoolManagement.Application.Features.Countrys.Requests.Commands
{
    public class DeleteCountryCommand : IRequest
    {
        public int CountryId { get; set; }
    }
}
