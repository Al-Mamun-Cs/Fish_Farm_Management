using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FisheriesInventoryOuts;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Commands;
using SchoolManagement.Application.Features.FisheriesInventoryOuts.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FisheriesInventoryOut)]
[ApiController]
[Authorize]
public class FisheriesInventoryOutController : ControllerBase
{
    private readonly IMediator _mediator;

    public FisheriesInventoryOutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-FisheriesInventoryOuts")]
    public async Task<ActionResult<PagedResult<FisheriesInventoryOutDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var FisheriesInventoryOuts = await _mediator.Send(new GetFisheriesInventoryOutListRequest { QueryParams = queryParams });
        return Ok(FisheriesInventoryOuts);
    }


    [HttpGet]
    [Route("get-FisheriesInventoryOutDetail/{id}")]
    public async Task<ActionResult<FisheriesInventoryOutDto>> Get(int id)
    {
        var FisheriesInventoryOut = await _mediator.Send(new GetFisheriesInventoryOutDetailRequest { FisheriesInventoryOutId = id });
        return Ok(FisheriesInventoryOut);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FisheriesInventoryOut")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFisheriesInventoryOutDto FisheriesInventoryOut)
    {
        var command = new CreateFisheriesInventoryOutCommand { FisheriesInventoryOutDto = FisheriesInventoryOut };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FisheriesInventoryOut/{id}")]
    public async Task<ActionResult> Put([FromBody] FisheriesInventoryOutDto FisheriesInventoryOut)
    {
        var command = new UpdateFisheriesInventoryOutCommand { FisheriesInventoryOutDto = FisheriesInventoryOut };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FisheriesInventoryOut/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFisheriesInventoryOutCommand { FisheriesInventoryOutId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedFisheriesInventoryOuts")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedFisheriesInventoryOut()
    {
        var selectedFisheriesInventoryOut = await _mediator.Send(new GetSelectedFisheriesInventoryOutRequest { });
        return Ok(selectedFisheriesInventoryOut);
    }

    
}

