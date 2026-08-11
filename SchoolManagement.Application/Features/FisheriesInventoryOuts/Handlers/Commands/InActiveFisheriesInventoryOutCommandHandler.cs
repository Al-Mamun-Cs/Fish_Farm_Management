using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventoryOuts.Handlers.Commands
{
    public class InActiveFisheriesInventoryOutCommandHandler : IRequestHandler<InActiveFisheriesInventoryOutCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveFisheriesInventoryOutCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveFisheriesInventoryOutCommand request, CancellationToken cancellationToken)
        {
            var FisheriesInventoryOut = await _unitOfWork.Repository<FisheriesInventoryOut>().Get(request.FisheriesInventoryOutId);
            

            FisheriesInventoryOut.ApproveStatus = true;

            if (FisheriesInventoryOut == null)
                throw new NotFoundException(nameof(FisheriesInventoryOut), request.FisheriesInventoryOutId);

            await _unitOfWork.Repository<FisheriesInventoryOut>().Update(FisheriesInventoryOut);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
