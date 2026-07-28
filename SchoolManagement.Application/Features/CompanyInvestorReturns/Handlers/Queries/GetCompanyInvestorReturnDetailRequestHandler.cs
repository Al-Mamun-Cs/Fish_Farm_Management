using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestorReturns.Handlers.Queries
{
    public class GetCompanyInvestorReturnDetailRequestHandler : IRequestHandler<GetCompanyInvestorReturnDetailRequest, CompanyInvestorReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<CompanyInvestorReturn> _CompanyInvestorReturnRepository;
        public GetCompanyInvestorReturnDetailRequestHandler(ISchoolManagementRepository<CompanyInvestorReturn> CompanyInvestorReturnRepository, IMapper mapper)
        {
            _CompanyInvestorReturnRepository = CompanyInvestorReturnRepository;
            _mapper = mapper;
        }
        public async Task<CompanyInvestorReturnDto> Handle(GetCompanyInvestorReturnDetailRequest request, CancellationToken cancellationToken)
        {
            var CompanyInvestorReturn = await _CompanyInvestorReturnRepository.Get(request.CompanyInvestorReturnId);
            return _mapper.Map<CompanyInvestorReturnDto>(CompanyInvestorReturn);
        }
    }
}
