using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Districts;
using SchoolManagement.Application.Features.Districts.Requests.Commands;
using SchoolManagement.Application.Features.Districts.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.District)]
[ApiController]
[Authorize]
public class DistrictController : ControllerBase
{
    private readonly IMediator _mediator;

    public DistrictController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Districts")]
    public async Task<ActionResult<PagedResult<DistrictDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Districts = await _mediator.Send(new GetDistrictListRequest { QueryParams = queryParams });
        return Ok(Districts);
    }

    

    [HttpGet]
    [Route("get-DistrictDetail/{id}")]
    public async Task<ActionResult<DistrictDto>> Get(int id)
    {
        var District = await _mediator.Send(new GetDistrictDetailRequest { DistrictId = id });
        return Ok(District);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-District")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDistrictDto District)
    {
        var command = new CreateDistrictCommand { DistrictDto = District };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-District/{id}")]
    public async Task<ActionResult> Put([FromBody] DistrictDto District)
    {
        var command = new UpdateDistrictCommand { DistrictDto = District };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-District/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDistrictCommand { DistrictId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDistrictForEshop")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDistrictForEshop()
    {
        var selectedDistrict = await _mediator.Send(new GetSelectedDistrictForEshopRequest { });
        return Ok(selectedDistrict);
    }

    [HttpGet]
    [Route("get-selectedDistricts")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDistrict(int DivisionId)
    {
        var selectedDistrict = await _mediator.Send(new GetSelectedDistrictRequest { DivisionId = DivisionId });
        return Ok(selectedDistrict);
    }

    [HttpGet]
    [Route("get-selectedDistrictForUpazila")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDistrictForUpazila()
    {
        var selectedDistrict = await _mediator.Send(new GetSelectedDistrictForUpazilaRequest {  });
        return Ok(selectedDistrict);
    }

    [HttpGet]
    [Route("get-AutoCompleteDistrictName")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteVoucerNo(string districtName)
    {
        var course = await _mediator.Send(new GetAutoCompleteDistrictRequest
        {
            DistrictName = districtName,
        });
        return Ok(course);
    }

}

