using MediatR;
using SchoolManagement.Application.DTOs.Banks;

namespace SchoolManagement.Application.Features.Banks.Requests.Commands
{
    public class UpdateBankCommand : IRequest<Unit>
    {
        public BankDto BankDto { get; set; }
    }
}
