using MediatR;
using SchoolManagement.Application.DTOs.Upozilas;

namespace SchoolManagement.Application.Features.Upozilas.Requests.Commands
{
    public class UpdateUpozilaCommand : IRequest<Unit>
    {
        public UpozilaDto UpozilaDto { get; set; }
    }
}
