using MediatR;

namespace SchoolManagement.Application.Features.Banks.Requests.Commands
{
    public class DeleteBankCommand : IRequest
    {
        public int BankId { get; set; }
    }
}
