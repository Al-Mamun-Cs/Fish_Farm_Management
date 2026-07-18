using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries
{
    public class GetAutoCompleteProductNameRequest : IRequest<List<SelectedModel>>
    {
        public string ProductName { get; set; }
        public int WarehouseId { get; set; }
        public int FisheriesProductTypeId { get; set; }
    }
}
