using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.SupplierTypes;
using SchoolManagement.Application.Features.SupplierTypes.Requests.Commands;
using SchoolManagement.Application.Features.SupplierTypes.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.SupplierType)]
[ApiController]
[Authorize]
public class SupplierTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ustomerTypes")]
    public async Task<ActionResult<PagedResult<SupplierTypeDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var SupplierTypes = await _mediator.Send(new GetSupplierTypeListRequest { QueryParams = queryParams });
        return Ok(SupplierTypes);
    }

    

    [HttpGet]
    [Route("get-SupplierTypeDetail/{id}")]
    public async Task<ActionResult<SupplierTypeDto>> Get(int id)
    {
        var SupplierType = await _mediator.Send(new GetSupplierTypeDetailRequest { SupplierTypeId = id });
        return Ok(SupplierType);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-SupplierType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSupplierTypeDto SupplierType)
    {
        var command = new CreateSupplierTypeCommand { SupplierTypeDto = SupplierType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-SupplierType/{id}")]
    public async Task<ActionResult> Put([FromBody] SupplierTypeDto SupplierType)
    {
        var command = new UpdateSupplierTypeCommand { SupplierTypeDto = SupplierType };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-SupplierType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSupplierTypeCommand { SupplierTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedSupplierTypes")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedSupplierType()
    {
        var selectedSupplierType = await _mediator.Send(new GetSelectedSupplierTypeRequest { });
        return Ok(selectedSupplierType);
    }

    
}

