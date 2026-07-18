using MediatR;
using SchoolManagement.Application.DTOs.Banks;

namespace SchoolManagement.Application.Features.Banks.Requests.Queries
{
    public class GetBankDetailRequest : IRequest<BankDto>
    {
        public int BankId { get; set; }
    }
}
