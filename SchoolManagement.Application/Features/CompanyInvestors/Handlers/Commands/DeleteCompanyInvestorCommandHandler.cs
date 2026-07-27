using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CompanyInvestors.Handlers.Commands
{
    public class DeleteCompanyInvestorCommandHandler : IRequestHandler<DeleteCompanyInvestorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCompanyInvestorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCompanyInvestorCommand request, CancellationToken cancellationToken)
        {
            var CompanyInvestor = await _unitOfWork.Repository<CompanyInvestor>().Get(request.CompanyInvestorId);

            if (CompanyInvestor == null)
                throw new NotFoundException(nameof(CompanyInvestor), request.CompanyInvestorId);


            try
            {
                await _unitOfWork.Repository<CompanyInvestor>().Delete(CompanyInvestor);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new NotFoundException("Data Can not deleted for relational attachment with other Tables!", request.CompanyInvestorId);
            }

            return Unit.Value;
        }
    }
}
