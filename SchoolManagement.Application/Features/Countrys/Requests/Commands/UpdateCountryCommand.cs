using MediatR;
using SchoolManagement.Application.DTOs.Countrys;

namespace SchoolManagement.Application.Features.Countrys.Requests.Commands
{
    public class UpdateCountryCommand : IRequest<Unit>
    {
        public CountryDto CountryDto { get; set; }
    }
}
