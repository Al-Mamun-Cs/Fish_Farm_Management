using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.BloodGroups;
using SchoolManagement.Application.Features.BloodGroups.Requests.Commands;
using SchoolManagement.Application.Features.BloodGroups.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.BloodGroup)]
[ApiController]
[Authorize]
public class BloodGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public BloodGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-BloodGroups")]
    public async Task<ActionResult<PagedResult<BloodGroupDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var BloodGroups = await _mediator.Send(new GetBloodGroupListRequest { QueryParams = queryParams });
        return Ok(BloodGroups);
    }

    

    [HttpGet]
    [Route("get-BloodGroupDetail/{id}")]
    public async Task<ActionResult<BloodGroupDto>> Get(int id)
    {
        var BloodGroup = await _mediator.Send(new GetBloodGroupDetailRequest { BloodGroupId = id });
        return Ok(BloodGroup);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-BloodGroup")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBloodGroupDto BloodGroup)
    {
        var command = new CreateBloodGroupCommand { BloodGroupDto = BloodGroup };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-BloodGroup/{id}")]
    public async Task<ActionResult> Put([FromBody] BloodGroupDto BloodGroup)
    {
        var command = new UpdateBloodGroupCommand { BloodGroupDto = BloodGroup };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-BloodGroup/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBloodGroupCommand { BloodGroupId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedBloodGroups")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedBloodGroup()
    {
        var selectedBloodGroup = await _mediator.Send(new GetSelectedBloodGroupRequest { });
        return Ok(selectedBloodGroup);
    }

    
}

