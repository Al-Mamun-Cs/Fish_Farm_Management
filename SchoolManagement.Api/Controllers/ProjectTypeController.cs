using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ProjectTypes;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Commands;
using SchoolManagement.Application.Features.ProjectTypes.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ProjectType)]
[ApiController]
[Authorize]
public class ProjectTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ProjectTypes")]
    public async Task<ActionResult<PagedResult<ProjectTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ProjectTypes = await _mediator.Send(new GetProjectTypeListRequest { QueryParams = queryParams });
        return Ok(ProjectTypes);
    }


    [HttpGet]
    [Route("get-ProjectTypeDetail/{id}")]
    public async Task<ActionResult<ProjectTypeDto>> Get(int id)
    {
        var ProjectType = await _mediator.Send(new GetProjectTypeDetailRequest { ProjectTypeId = id });
        return Ok(ProjectType);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ProjectType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateProjectTypeDto ProjectType)
    {
        var command = new CreateProjectTypeCommand { ProjectTypeDto = ProjectType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ProjectType/{id}")]
    public async Task<ActionResult> Put([FromBody] ProjectTypeDto ProjectType)
    {
        var command = new UpdateProjectTypeCommand { ProjectTypeDto = ProjectType };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ProjectType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteProjectTypeCommand { ProjectTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedProjectTypes")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedProjectType()
    {
        var selectedProjectType = await _mediator.Send(new GetSelectedProjectTypeRequest { });
        return Ok(selectedProjectType);
    }

    


}

