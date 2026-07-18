using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Fiscalyears;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Commands;
using SchoolManagement.Application.Features.Fiscalyears.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Fiscalyear)]
[ApiController]
[Authorize]
public class FiscalyearController : ControllerBase
{
    private readonly IMediator _mediator;

    public FiscalyearController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Fiscalyears")]
    public async Task<ActionResult<PagedResult<FiscalyearDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Fiscalyears = await _mediator.Send(new GetFiscalyearListRequest { QueryParams = queryParams });
        return Ok(Fiscalyears);
    }

    

    [HttpGet]
    [Route("get-FiscalyearDetail/{id}")]
    public async Task<ActionResult<FiscalyearDto>> Get(int id)
    {
        var Fiscalyear = await _mediator.Send(new GetFiscalyearDetailRequest { FiscalyearId = id });
        return Ok(Fiscalyear);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Fiscalyear")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFiscalyearDto Fiscalyear)
    {
        var command = new CreateFiscalyearCommand { FiscalyearDto = Fiscalyear };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Fiscalyear/{id}")]
    public async Task<ActionResult> Put([FromBody] FiscalyearDto Fiscalyear)
    {
        var command = new UpdateFiscalyearCommand { FiscalyearDto = Fiscalyear };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Fiscalyear/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFiscalyearCommand { FiscalyearId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedFiscalyears")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedFiscalyear()
    {
        var selectedFiscalyear = await _mediator.Send(new GetSelectedFiscalyearRequest { });
        return Ok(selectedFiscalyear);
    }

    
}

