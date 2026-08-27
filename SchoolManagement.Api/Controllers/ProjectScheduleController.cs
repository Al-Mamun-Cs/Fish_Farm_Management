using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ProjectSchedules;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Commands;
using SchoolManagement.Application.Features.ProjectSchedules.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ProjectSchedule)]
[ApiController]
[Authorize]
public class ProjectScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ProjectSchedules")]
    public async Task<ActionResult<PagedResult<ProjectScheduleDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ProjectSchedules = await _mediator.Send(new GetProjectScheduleListRequest { QueryParams = queryParams });
        return Ok(ProjectSchedules);
    }


    [HttpGet]
    [Route("get-ProjectScheduleDetail/{id}")]
    public async Task<ActionResult<ProjectScheduleDto>> Get(int id)
    {
        var ProjectSchedule = await _mediator.Send(new GetProjectScheduleDetailRequest { ProjectScheduleId = id });
        return Ok(ProjectSchedule);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ProjectSchedule")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProjectScheduleDto ProjectSchedule)
    {
        var command = new CreateProjectScheduleCommand { ProjectScheduleDto = ProjectSchedule };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ProjectSchedule/{id}")]
    public async Task<ActionResult> Put([FromBody] ProjectScheduleDto ProjectSchedule)
    {
        var command = new UpdateProjectScheduleCommand { ProjectScheduleDto = ProjectSchedule };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ProjectSchedule/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProjectScheduleCommand { ProjectScheduleId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedProjectSchedules")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedProjectSchedule()
    {
        var selectedProjectSchedule = await _mediator.Send(new GetSelectedProjectScheduleRequest { });
        return Ok(selectedProjectSchedule);
    }

    
}

