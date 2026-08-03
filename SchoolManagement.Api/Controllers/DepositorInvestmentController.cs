using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.DepositorInvestments;
using SchoolManagement.Application.Features.DepositorInstallments.Requests.Queries;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Commands;
using SchoolManagement.Application.Features.DepositorInvestments.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.DepositorInvestment)]
[ApiController]
[Authorize]
public class DepositorInvestmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepositorInvestmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-DepositorInvestments")]
    public async Task<ActionResult<PagedResult<DepositorInvestmentDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var DepositorInvestments = await _mediator.Send(new GetDepositorInvestmentListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(DepositorInvestments);
    }


    [HttpGet]
    [Route("get-DepositorInvestmentDetail/{id}")]
    public async Task<ActionResult<DepositorInvestmentDto>> Get(int id)
    {
        var DepositorInvestment = await _mediator.Send(new GetDepositorInvestmentDetailRequest { DepositorInvestmentId = id });
        return Ok(DepositorInvestment);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-DepositorInvestment")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateDepositorInvestmentDto DepositorInvestment)
    {
        var command = new CreateDepositorInvestmentCommand { DepositorInvestmentDto = DepositorInvestment };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-DepositorInvestment/{id}")]
    public async Task<ActionResult> Put([FromBody] DepositorInvestmentDto DepositorInvestment)
    {
        var command = new UpdateDepositorInvestmentCommand { DepositorInvestmentDto = DepositorInvestment };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-DepositorInvestment/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteDepositorInvestmentCommand { DepositorInvestmentId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedDepositorInvestments")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedDepositorInvestment(int warehouseId)
    {
        var selectedDepositorInvestment = await _mediator.Send(new GetSelectedDepositorInvestmentRequest {WarehouseId = warehouseId });
        return Ok(selectedDepositorInvestment);
    }

   

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-DepositorInvestment/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveDepositorInvestmentCommand { DepositorInvestmentId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-SpGetTotalDepositorInvestment")]
    public async Task<ActionResult> SpGetTotalDepositorInvestment(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalDepositorInvestmentRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetDepstInvestmentDetail")]
    public async Task<ActionResult> SpGetDepstInvestmentDetail(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetDepstInvestmentDetailRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("close-DepositorInvestment/{id}")]
    public async Task<ActionResult> CloseDepositorInvestment(int id)
    {
        var command = new CloseDepositorInvestmentCommand { DepositorInvestmentId = id };
        await _mediator.Send(command);
        return NoContent();
    }

}

