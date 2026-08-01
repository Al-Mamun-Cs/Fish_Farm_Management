using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Depositors;
using SchoolManagement.Application.Features.Depositors.Requests.Commands;
using SchoolManagement.Application.Features.Depositors.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Depositor)]
[ApiController]
[Authorize]
public class DepositorController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepositorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Depositors")]
    public async Task<ActionResult<PagedResult<DepositorDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var Depositors = await _mediator.Send(new GetDepositorListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(Depositors);
    }


    [HttpGet]
    [Route("get-DepositorDetail/{id}")]
    public async Task<ActionResult<DepositorDto>> Get(int id)
    {
        var Depositor = await _mediator.Send(new GetDepositorDetailRequest { DepositorId = id });
        return Ok(Depositor);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Depositor")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDepositorDto Depositor)
    {
        var command = new CreateDepositorCommand { DepositorDto = Depositor };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Depositor/{id}")]
    public async Task<ActionResult> Put([FromBody] DepositorDto Depositor)
    {
        var command = new UpdateDepositorCommand { DepositorDto = Depositor };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Depositor/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDepositorCommand { DepositorId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDepositors")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDepositor(int warehouseId)
    {
        var selectedDepositor = await _mediator.Send(new GetSelectedDepositorRequest { WarehouseId = warehouseId});
        return Ok(selectedDepositor);
    }



}

