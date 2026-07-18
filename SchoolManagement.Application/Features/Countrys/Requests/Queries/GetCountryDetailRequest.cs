using MediatR;
using SchoolManagement.Application.DTOs.Countrys;

namespace SchoolManagement.Application.Features.Countrys.Requests.Queries
{
    public class GetCountryDetailRequest : IRequest<CountryDto>
    {
        public int CountryId { get; set; }
    }
}
