using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Suppliers.Handlers.Queries
{
    public class GetPhoneNoIsExistCheckRequestHandler : IRequestHandler<GetPhoneNoIsExistCheckRequest, bool>
    {
        private readonly ISchoolManagementRepository<Supplier> _SupplierRepository; 
        public GetPhoneNoIsExistCheckRequestHandler(ISchoolManagementRepository<Supplier> SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;
        }
          
        public async Task<bool> Handle(GetPhoneNoIsExistCheckRequest request, CancellationToken cancellationToken)
        {
            ICollection<Supplier> bookList = await _SupplierRepository.FilterAsync(x => x.IsActive);
            bool isExist = bookList.Any(x => x.PhoneNo == request.PhoneNo);
            if (isExist)
            {
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }
      }
}
