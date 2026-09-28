using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands
{
    public class DeleteEasyBikeBankCommand : IRequest
    {
        public int EasyBikeBankId { get; set; }
    }
}
