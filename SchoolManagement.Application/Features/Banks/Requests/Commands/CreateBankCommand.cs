using MediatR;
using SchoolManagement.Application.DTOs.Banks;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Banks.Requests.Commands
{
    public class CreateBankCommand : IRequest<BaseCommandResponse>
    {
        public CreateBankDto BankDto { get; set; }
    }
}
