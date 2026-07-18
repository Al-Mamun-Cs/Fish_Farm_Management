using MediatR;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class GetAutoCompleteForBankTransactionRequest : IRequest<List<SelectedModel>>
    {
        public string SupplierName { get; set; }
        public int WarehouseId { get; set; }
    }
}
