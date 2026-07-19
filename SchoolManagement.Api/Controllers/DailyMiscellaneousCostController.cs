using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DailyMiscellaneousCosts;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Commands;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DailyMiscellaneousCost)]
[ApiController]
[Authorize]
public class DailyMiscellaneousCostController : ControllerBase
{
    private readonly IMediator _mediator;

    public DailyMiscellaneousCostController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-DailyMiscellaneousCosts")]
    public async Task<ActionResult<PagedResult<DailyMiscellaneousCostDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var DailyMiscellaneousCosts = await _mediator.Send(new GetDailyMiscellaneousCostListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(DailyMiscellaneousCosts);
    }


    [HttpGet]
    [Route("get-DailyMiscellaneousCostDetail/{id}")]
    public async Task<ActionResult<DailyMiscellaneousCostDto>> Get(int id)
    {
        var DailyMiscellaneousCost = await _mediator.Send(new GetDailyMiscellaneousCostDetailRequest { DailyMiscellaneousCostId = id });
        return Ok(DailyMiscellaneousCost);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DailyMiscellaneousCost")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDailyMiscellaneousCostDto DailyMiscellaneousCost)
    {
        var command = new CreateDailyMiscellaneousCostCommand { DailyMiscellaneousCostDto = DailyMiscellaneousCost };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DailyMiscellaneousCost/{id}")]
    public async Task<ActionResult> Put([FromBody] DailyMiscellaneousCostDto DailyMiscellaneousCost)
    {
        var command = new UpdateDailyMiscellaneousCostCommand { DailyMiscellaneousCostDto = DailyMiscellaneousCost };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DailyMiscellaneousCost/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDailyMiscellaneousCostCommand { DailyMiscellaneousCostId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDailyMiscellaneousCosts")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDailyMiscellaneousCost()
    {
        var selectedDailyMiscellaneousCost = await _mediator.Send(new GetSelectedDailyMiscellaneousCostRequest { });
        return Ok(selectedDailyMiscellaneousCost);
    }

    
}

