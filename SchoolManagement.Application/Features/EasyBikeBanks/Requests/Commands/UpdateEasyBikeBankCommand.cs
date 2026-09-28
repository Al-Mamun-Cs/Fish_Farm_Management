using MediatR;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands
{
    public class UpdateEasyBikeBankCommand : IRequest<Unit>
    {
        public EasyBikeBankDto EasyBikeBankDto { get; set; }
    }
}
