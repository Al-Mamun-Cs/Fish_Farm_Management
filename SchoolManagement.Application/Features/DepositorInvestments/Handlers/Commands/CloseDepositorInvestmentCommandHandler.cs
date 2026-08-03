using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DepositorInvestments.Handlers.Commands
{
    public class CloseDepositorInvestmentCommandHandler : IRequestHandler<CloseDepositorInvestmentCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CloseDepositorInvestmentCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CloseDepositorInvestmentCommand request, CancellationToken cancellationToken)
        {
            var DepositorInvestment = await _unitOfWork.Repository<DepositorInvestment>().Get(request.DepositorInvestmentId);
            

            DepositorInvestment.CloseStatus = 1;

            if (DepositorInvestment == null)
                throw new NotFoundException(nameof(DepositorInvestment), request.DepositorInvestmentId);

            await _unitOfWork.Repository<DepositorInvestment>().Update(DepositorInvestment);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
