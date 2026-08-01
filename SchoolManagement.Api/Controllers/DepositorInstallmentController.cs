using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DepositorInstallments;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Commands;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DepositorInstallment)]
[ApiController]
[Authorize]
public class DepositorInstallmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepositorInstallmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-DepositorInstallments")]
    public async Task<ActionResult<PagedResult<DepositorInstallmentDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var DepositorInstallments = await _mediator.Send(new GetDepositorInstallmentListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(DepositorInstallments);
    }


    [HttpGet]
    [Route("get-DepositorInstallmentDetail/{id}")]
    public async Task<ActionResult<DepositorInstallmentDto>> Get(int id)
    {
        var DepositorInstallment = await _mediator.Send(new GetDepositorInstallmentDetailRequest { DepositorInstallmentId = id });
        return Ok(DepositorInstallment);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DepositorInstallment")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDepositorInstallmentDto DepositorInstallment)
    {
        var command = new CreateDepositorInstallmentCommand { DepositorInstallmentDto = DepositorInstallment };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DepositorInstallment/{id}")]
    public async Task<ActionResult> Put([FromBody] DepositorInstallmentDto DepositorInstallment)
    {
        var command = new UpdateDepositorInstallmentCommand { DepositorInstallmentDto = DepositorInstallment };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DepositorInstallment/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDepositorInstallmentCommand { DepositorInstallmentId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDepositorInstallments")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDepositorInstallment()
    {
        var selectedDepositorInstallment = await _mediator.Send(new GetSelectedDepositorInstallmentRequest { });
        return Ok(selectedDepositorInstallment);
    }

   

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-DepositorInstallment/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveDepositorInstallmentCommand { DepositorInstallmentId = id };
        await _mediator.Send(command);
        return NoContent();
    }


}

