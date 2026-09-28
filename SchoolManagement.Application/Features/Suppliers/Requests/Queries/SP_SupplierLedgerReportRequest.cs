using MediatR;

namespace SchoolManagement.Application.Features.Suppliers.Requests.Queries
{
    public class SP_SupplierLedgerReportRequest : IRequest<object>
    {
        public int SupplierId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        
    }
}
