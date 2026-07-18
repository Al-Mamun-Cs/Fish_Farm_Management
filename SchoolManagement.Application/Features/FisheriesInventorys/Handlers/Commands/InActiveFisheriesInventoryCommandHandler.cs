using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.FisheriesInventorys.Handlers.Commands
{
    public class InActiveFisheriesInventoryCommandHandler : IRequestHandler<InActiveFisheriesInventoryCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveFisheriesInventoryCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveFisheriesInventoryCommand request, CancellationToken cancellationToken)
        {
            var FisheriesInventory = await _unitOfWork.Repository<FisheriesInventory>().Get(request.FisheriesInventoryId);


            FisheriesInventory.ApproveStatus = true;

            if (FisheriesInventory == null)
                throw new NotFoundException(nameof(FisheriesInventory), request.FisheriesInventoryId);

            await _unitOfWork.Repository<FisheriesInventory>().Update(FisheriesInventory);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
