using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FisheriesProductReturns;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Commands;
using SchoolManagement.Application.Features.FisheriesProductReturns.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FisheriesProductReturn)]
[ApiController]
[Authorize]
public class FisheriesProductReturnController : ControllerBase
{
    private readonly IMediator _mediator;

    public FisheriesProductReturnController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-FisheriesProductReturns")]
    public async Task<ActionResult<PagedResult<FisheriesProductReturnDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var FisheriesProductReturns = await _mediator.Send(new GetFisheriesProductReturnListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(FisheriesProductReturns);
    }


    [HttpGet]
    [Route("get-FisheriesProductReturnDetail/{id}")]
    public async Task<ActionResult<FisheriesProductReturnDto>> Get(int id)
    {
        var FisheriesProductReturn = await _mediator.Send(new GetFisheriesProductReturnDetailRequest { FisheriesProductReturnId = id });
        return Ok(FisheriesProductReturn);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FisheriesProductReturn")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFisheriesProductReturnDto FisheriesProductReturn)
    {
        var command = new CreateFisheriesProductReturnCommand { FisheriesProductReturnDto = FisheriesProductReturn };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FisheriesProductReturn/{id}")]
    public async Task<ActionResult> Put([FromBody] FisheriesProductReturnDto FisheriesProductReturn)
    {
        var command = new UpdateFisheriesProductReturnCommand { FisheriesProductReturnDto = FisheriesProductReturn };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FisheriesProductReturn/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFisheriesProductReturnCommand { FisheriesProductReturnId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedFisheriesProductReturns")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedFisheriesProductReturn(int warehouseId)
    {
        var selectedFisheriesProductReturn = await _mediator.Send(new GetSelectedFisheriesProductReturnRequest {WarehouseId = warehouseId });
        return Ok(selectedFisheriesProductReturn);
    }

   

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-FisheriesProductReturn/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveFisheriesProductReturnCommand { FisheriesProductReturnId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    

    

}

