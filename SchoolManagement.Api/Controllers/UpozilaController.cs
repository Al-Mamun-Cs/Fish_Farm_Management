using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Upozilas;
using SchoolManagement.Application.Features.Districts.Requests.Queries;
using SchoolManagement.Application.Features.Upozilas.Requests.Commands;
using SchoolManagement.Application.Features.Upozilas.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Upozila)]
[ApiController]
[Authorize]
public class UpozilaController : ControllerBase
{
    private readonly IMediator _mediator;

    public UpozilaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Upozilas")]
    public async Task<ActionResult<PagedResult<UpozilaDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Upozilas = await _mediator.Send(new GetUpozilaListRequest { QueryParams = queryParams });
        return Ok(Upozilas);
    }

    

    [HttpGet]
    [Route("get-UpozilaDetail/{id}")]
    public async Task<ActionResult<UpozilaDto>> Get(int id)
    {
        var Upozila = await _mediator.Send(new GetUpozilaDetailRequest { UpazilaId = id });
        return Ok(Upozila);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Upozila")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUpozilaDto Upozila)
    {
        var command = new CreateUpozilaCommand { UpozilaDto = Upozila };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Upozila/{id}")]
    public async Task<ActionResult> Put([FromBody] UpozilaDto Upozila)
    {
        var command = new UpdateUpozilaCommand { UpozilaDto = Upozila };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Upozila/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteUpozilaCommand { UpazilaId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedUpozilas")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedUpozila(int districtId)
    {
        var selectedUpozila = await _mediator.Send(new GetSelectedUpozilaRequest { DistrictId = districtId });
        return Ok(selectedUpozila);
    }
    [HttpGet]
    [Route("get-AutoCompleteUpazilaName")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteVoucerNo(string upazilaName)
    {
        var course = await _mediator.Send(new GetAutoCompleteUpazilaNameRequest
        {
            UpazilaName = upazilaName,
        });
        return Ok(course);
    }

}

