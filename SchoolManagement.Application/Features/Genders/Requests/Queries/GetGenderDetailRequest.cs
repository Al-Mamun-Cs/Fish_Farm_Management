using MediatR;
using SchoolManagement.Application.DTOs.Genders;

namespace SchoolManagement.Application.Features.Genders.Requests.Queries
{
    public class GetGenderDetailRequest : IRequest<GenderDto>
    {
        public int GenderId { get; set; }
    }
}
