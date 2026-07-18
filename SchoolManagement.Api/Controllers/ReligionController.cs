using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Religions;
using SchoolManagement.Application.Features.Religions.Requests.Commands;
using SchoolManagement.Application.Features.Religions.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Religion)]
[ApiController]
[Authorize]
public class ReligionController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReligionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Religions")]
    public async Task<ActionResult<PagedResult<ReligionDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Religions = await _mediator.Send(new GetReligionListRequest { QueryParams = queryParams });
        return Ok(Religions);
    }

    

    [HttpGet]
    [Route("get-ReligionDetail/{id}")]
    public async Task<ActionResult<ReligionDto>> Get(int id)
    {
        var Religion = await _mediator.Send(new GetReligionDetailRequest { ReligionId = id });
        return Ok(Religion);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Religion")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateReligionDto Religion)
    {
        var command = new CreateReligionCommand { ReligionDto = Religion };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Religion/{id}")]
    public async Task<ActionResult> Put([FromBody] ReligionDto Religion)
    {
        var command = new UpdateReligionCommand { ReligionDto = Religion };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Religion/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteReligionCommand { ReligionId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedReligions")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedReligion()
    {
        var selectedReligion = await _mediator.Send(new GetSelectedReligionRequest { });
        return Ok(selectedReligion);
    }

    
}

