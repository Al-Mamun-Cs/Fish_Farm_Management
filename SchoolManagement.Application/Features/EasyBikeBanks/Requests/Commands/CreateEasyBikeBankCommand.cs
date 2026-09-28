using MediatR;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using SchoolManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands
{
    public class CreateEasyBikeBankCommand : IRequest<BaseCommandResponse>
    {
        public CreateEasyBikeBankDto EasyBikeBankDto { get; set; }
    }
}
