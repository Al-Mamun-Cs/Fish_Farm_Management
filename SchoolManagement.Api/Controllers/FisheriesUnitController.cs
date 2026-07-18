using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FisheriesUnits;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Commands;
using SchoolManagement.Application.Features.FisheriesUnits.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FisheriesUnit)]
[ApiController]
[Authorize]
public class FisheriesUnitController : ControllerBase
{
    private readonly IMediator _mediator;

    public FisheriesUnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-FisheriesUnits")]
    public async Task<ActionResult<PagedResult<FisheriesUnitDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var FisheriesUnits = await _mediator.Send(new GetFisheriesUnitListRequest { QueryParams = queryParams });
        return Ok(FisheriesUnits);
    }


    [HttpGet]
    [Route("get-FisheriesUnitDetail/{id}")]
    public async Task<ActionResult<FisheriesUnitDto>> Get(int id)
    {
        var FisheriesUnit = await _mediator.Send(new GetFisheriesUnitDetailRequest { FisheriesUnitId = id });
        return Ok(FisheriesUnit);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FisheriesUnit")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFisheriesUnitDto FisheriesUnit)
    {
        var command = new CreateFisheriesUnitCommand { FisheriesUnitDto = FisheriesUnit };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FisheriesUnit/{id}")]
    public async Task<ActionResult> Put([FromBody] FisheriesUnitDto FisheriesUnit)
    {
        var command = new UpdateFisheriesUnitCommand { FisheriesUnitDto = FisheriesUnit };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FisheriesUnit/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFisheriesUnitCommand { FisheriesUnitId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedFisheriesUnits")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedFisheriesUnit()
    {
        var selectedFisheriesUnit = await _mediator.Send(new GetSelectedFisheriesUnitRequest { });
        return Ok(selectedFisheriesUnit);
    }

    
}

