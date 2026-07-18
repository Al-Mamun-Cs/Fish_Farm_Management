using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries
{
    public class SpGetFisheriesInventoryVoucherByIdRequest : IRequest<object>
    {
        public int? FisheriesInventoryId { get; set; }
    }
}
  