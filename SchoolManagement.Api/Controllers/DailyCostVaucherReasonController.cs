using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DailyCostVaucherReasons;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Commands;
using SchoolManagement.Application.Features.DailyCostVaucherReasons.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DailyCostVaucherReason)]
[ApiController]
[Authorize]
public class DailyCostVaucherReasonController : ControllerBase
{
    private readonly IMediator _mediator;

    public DailyCostVaucherReasonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-DailyCostVaucherReasons")]
    public async Task<ActionResult<PagedResult<DailyCostVaucherReasonDto>>> Get([FromQuery] QueryParams queryParams,int warehouseId)
    {
        var DailyCostVaucherReasons = await _mediator.Send(new GetDailyCostVaucherReasonListRequest { QueryParams = queryParams,WarehouseId= warehouseId });
        return Ok(DailyCostVaucherReasons);
    }


    [HttpGet]
    [Route("get-DailyCostVaucherReasonDetail/{id}")]
    public async Task<ActionResult<DailyCostVaucherReasonDto>> Get(int id)
    {
        var DailyCostVaucherReason = await _mediator.Send(new GetDailyCostVaucherReasonDetailRequest { DailyCostVaucherReasonId = id });
        return Ok(DailyCostVaucherReason);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DailyCostVaucherReason")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDailyCostVaucherReasonDto DailyCostVaucherReason)
    {
        var command = new CreateDailyCostVaucherReasonCommand { DailyCostVaucherReasonDto = DailyCostVaucherReason };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DailyCostVaucherReason/{id}")]
    public async Task<ActionResult> Put([FromBody] DailyCostVaucherReasonDto DailyCostVaucherReason)
    {
        var command = new UpdateDailyCostVaucherReasonCommand { DailyCostVaucherReasonDto = DailyCostVaucherReason };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DailyCostVaucherReason/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDailyCostVaucherReasonCommand { DailyCostVaucherReasonId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDailyCostVaucherReasons")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDailyCostVaucherReason( int warehouseId)
    {
        var selectedDailyCostVaucherReason = await _mediator.Send(new GetSelectedDailyCostVaucherReasonRequest {WarehouseId = warehouseId });
        return Ok(selectedDailyCostVaucherReason);
    }

    
}

