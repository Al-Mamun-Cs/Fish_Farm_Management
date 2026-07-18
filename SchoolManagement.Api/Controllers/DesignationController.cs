using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Designations;
using SchoolManagement.Application.Features.Designations.Requests.Commands;
using SchoolManagement.Application.Features.Designations.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Designation)]
[ApiController]
[Authorize]
public class DesignationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DesignationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Designations")]
    public async Task<ActionResult<PagedResult<DesignationDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Designations = await _mediator.Send(new GetDesignationListRequest { QueryParams = queryParams });
        return Ok(Designations);
    }

    

    [HttpGet]
    [Route("get-DesignationDetail/{id}")]
    public async Task<ActionResult<DesignationDto>> Get(int id)
    {
        var Designation = await _mediator.Send(new GetDesignationDetailRequest { DesignationId = id });
        return Ok(Designation);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Designation")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDesignationDto Designation)
    {
        var command = new CreateDesignationCommand { DesignationDto = Designation };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Designation/{id}")]
    public async Task<ActionResult> Put([FromBody] DesignationDto Designation)
    {
        var command = new UpdateDesignationCommand { DesignationDto = Designation };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Designation/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDesignationCommand { DesignationId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDesignations")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDesignation(int warehouseId)
    {
        var selectedDesignation = await _mediator.Send(new GetSelectedDesignationRequest { WarehouseId = warehouseId});
        return Ok(selectedDesignation);
    }


    [HttpGet]
    [Route("get-selectedDesignationsForManagement")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDesignationForManagement()
    {
        var selectedDesignation = await _mediator.Send(new GetSelectedDesignationForManagementRequest { });
        return Ok(selectedDesignation);
    }

}

