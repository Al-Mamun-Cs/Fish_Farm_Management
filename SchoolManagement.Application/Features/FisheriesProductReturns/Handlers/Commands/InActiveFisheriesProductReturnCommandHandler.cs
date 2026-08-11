using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesProductReturns.Handlers.Commands
{
    public class InActiveFisheriesProductReturnCommandHandler : IRequestHandler<InActiveFisheriesProductReturnCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveFisheriesProductReturnCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveFisheriesProductReturnCommand request, CancellationToken cancellationToken)
        {
            var FisheriesProductReturn = await _unitOfWork.Repository<FisheriesProductReturn>().Get(request.FisheriesProductReturnId);
            // Start username 
            var uid = _httpContextAccessor.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new Exception("User is not authenticated.");
            var user = await _userService.GetUserById(uid);
            var username = user.UserName;
            // End username

            FisheriesProductReturn.ApproveStatus = 1;
            FisheriesProductReturn.ApproveDate = DateTime.Now;
            FisheriesProductReturn.ApproveBy = username;

            if (FisheriesProductReturn == null)
                throw new NotFoundException(nameof(FisheriesProductReturn), request.FisheriesProductReturnId);

            await _unitOfWork.Repository<FisheriesProductReturn>().Update(FisheriesProductReturn);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
