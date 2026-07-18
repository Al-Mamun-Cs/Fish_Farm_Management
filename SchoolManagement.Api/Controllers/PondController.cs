using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Ponds;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Features.Ponds.Requests.Commands;
using SchoolManagement.Application.Features.Ponds.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Pond)]
[ApiController]
[Authorize]
public class PondController : ControllerBase
{
    private readonly IMediator _mediator;

    public PondController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Ponds")]
    public async Task<ActionResult<PagedResult<PondDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Ponds = await _mediator.Send(new GetPondListRequest { QueryParams = queryParams });
        return Ok(Ponds);
    }


    [HttpGet]
    [Route("get-PondDetail/{id}")]
    public async Task<ActionResult<PondDto>> Get(int id)
    {
        var Pond = await _mediator.Send(new GetPondDetailRequest { PondId = id });
        return Ok(Pond);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Pond")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePondDto Pond)
    {
        var command = new CreatePondCommand { PondDto = Pond };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Pond/{id}")]
    public async Task<ActionResult> Put([FromBody] PondDto Pond)
    {
        var command = new UpdatePondCommand { PondDto = Pond };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Pond/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePondCommand { PondId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedPonds")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedPond()
    {
        var selectedPond = await _mediator.Send(new GetSelectedPondRequest { });
        return Ok(selectedPond);
    }

    [HttpGet]
    [Route("get-SpGetTotalFisheriesPondList")]
    public async Task<ActionResult> SpGetTotalFisheriesPondList(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalFisheriesPondListRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetTotalFisheriesProductTypewiseCoset")]
    public async Task<ActionResult> SpGetTotalFisheriesProductTypewiseCoset(int warehouseId,int pondId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalFisheriesProductTypewiseCosetRequest
        {
            WarehouseId = warehouseId,
            PondId = pondId
        });
        return Ok(easyBikeListByType);
    }


}

