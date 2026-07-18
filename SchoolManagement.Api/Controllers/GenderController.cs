using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Genders;
using SchoolManagement.Application.Features.Genders.Requests.Commands;
using SchoolManagement.Application.Features.Genders.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Gender)]
[ApiController]
[Authorize]
public class GenderController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Genders")]
    public async Task<ActionResult<PagedResult<GenderDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Genders = await _mediator.Send(new GetGenderListRequest { QueryParams = queryParams });
        return Ok(Genders);
    }

    

    [HttpGet]
    [Route("get-GenderDetail/{id}")]
    public async Task<ActionResult<GenderDto>> Get(int id)
    {
        var Gender = await _mediator.Send(new GetGenderDetailRequest { GenderId = id });
        return Ok(Gender);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Gender")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateGenderDto Gender)
    {
        var command = new CreateGenderCommand { GenderDto = Gender };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Gender/{id}")]
    public async Task<ActionResult> Put([FromBody] GenderDto Gender)
    {
        var command = new UpdateGenderCommand { GenderDto = Gender };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Gender/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteGenderCommand { GenderId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedGenders")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedGender()
    {
        var selectedGender = await _mediator.Send(new GetSelectedGenderRequest { });
        return Ok(selectedGender);
    }

    
}

