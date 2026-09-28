using MediatR;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries
{
    public class GetEasyBikeBankDetailRequest : IRequest<EasyBikeBankDto>
    {
        public int EasyBikeBankId { get; set; }
    }
}
