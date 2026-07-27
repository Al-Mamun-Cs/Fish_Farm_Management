using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CompanyInvestors;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Queries
{
    public class GetCompanyInvestorDetailRequestHandler : IRequestHandler<GetCompanyInvestorDetailRequest, CompanyInvestorDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<CompanyInvestor> _CompanyInvestorRepository;
        public GetCompanyInvestorDetailRequestHandler(ISchoolManagementRepository<CompanyInvestor> CompanyInvestorRepository, IMapper mapper)
        {
            _CompanyInvestorRepository = CompanyInvestorRepository;
            _mapper = mapper;
        }
        public async Task<CompanyInvestorDto> Handle(GetCompanyInvestorDetailRequest request, CancellationToken cancellationToken)
        {
            var CompanyInvestor = await _CompanyInvestorRepository.Get(request.CompanyInvestorId);
            return _mapper.Map<CompanyInvestorDto>(CompanyInvestor);
        }
    }
}
