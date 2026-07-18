using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetPhoneNoIsExistCheckRequest : IRequest<bool>
    {
        public string PhoneNo { get; set; }
    }
}
 