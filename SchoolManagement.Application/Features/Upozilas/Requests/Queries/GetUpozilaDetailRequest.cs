using MediatR;
using SchoolManagement.Application.DTOs.Upozilas;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Queries
{
    public class GetUpozilaDetailRequest : IRequest<UpozilaDto>
    {
        public int UpazilaId { get; set; }
    }
}
