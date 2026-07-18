using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Countrys;
using SchoolManagement.Application.Features.Countrys.Requests.Commands;
using SchoolManagement.Application.Features.Countrys.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Country)]
[ApiController]
[Authorize]
public class CountryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Countrys")]
    public async Task<ActionResult<PagedResult<CountryDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Countrys = await _mediator.Send(new GetCountryListRequest { QueryParams = queryParams });
        return Ok(Countrys);
    }

    

    [HttpGet]
    [Route("get-CountryDetail/{id}")]
    public async Task<ActionResult<CountryDto>> Get(int id)
    {
        var Country = await _mediator.Send(new GetCountryDetailRequest { CountryId = id });
        return Ok(Country);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Country")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCountryDto Country)
    {
        var command = new CreateCountryCommand { CountryDto = Country };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Country/{id}")]
    public async Task<ActionResult> Put([FromBody] CountryDto Country)
    {
        var command = new UpdateCountryCommand { CountryDto = Country };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Country/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCountryCommand { CountryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedCountrys")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedCountry()
    {
        var selectedCountry = await _mediator.Send(new GetSelectedCountryRequest { });
        return Ok(selectedCountry);
    }

    [HttpGet]
    [Route("get-AutoCompleteCountry")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteCountry(string name)
    {
        var course = await _mediator.Send(new GetAutoCompleteCountryRequest
        {
            Name = name,
        });
        return Ok(course);
    }


}

