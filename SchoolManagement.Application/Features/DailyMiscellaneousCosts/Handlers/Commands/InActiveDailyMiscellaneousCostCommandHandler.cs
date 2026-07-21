using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.Contracts.Identity;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.DailyMiscellaneousCosts.Handlers.Commands
{
    public class InActiveDailyMiscellaneousCostCommandHandler : IRequestHandler<InActiveDailyMiscellaneousCostCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InActiveDailyMiscellaneousCostCommandHandler(IUserService userService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(InActiveDailyMiscellaneousCostCommand request, CancellationToken cancellationToken)
        {
            var DailyMiscellaneousCost = await _unitOfWork.Repository<DailyMiscellaneousCost>().Get(request.DailyMiscellaneousCostId);


            DailyMiscellaneousCost.ApprovedStatus = 1;

            if (DailyMiscellaneousCost == null)
                throw new NotFoundException(nameof(DailyMiscellaneousCost), request.DailyMiscellaneousCostId);

            await _unitOfWork.Repository<DailyMiscellaneousCost>().Update(DailyMiscellaneousCost);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
