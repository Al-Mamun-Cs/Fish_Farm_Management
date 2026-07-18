using MediatR;
using SchoolManagement.Application.DTOs.Countrys;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Countrys.Requests.Commands
{
    public class CreateCountryCommand : IRequest<BaseCommandResponse>
    {
        public CreateCountryDto CountryDto { get; set; }
    }
}
