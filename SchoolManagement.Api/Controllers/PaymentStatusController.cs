using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.PaymentStatuses;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Commands;
using SchoolManagement.Application.Features.PaymentStatuses.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.PaymentStatus)]
[ApiController]
[Authorize]
public class PaymentStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-paymentStatuss")]
    public async Task<ActionResult<PagedResult<PaymentStatusDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var PaymentStatuss = await _mediator.Send(new GetPaymentStatusListRequest { QueryParams = queryParams });
        return Ok(PaymentStatuss);
    }

    

    [HttpGet]
    [Route("get-paymentStatusDetail/{id}")]
    public async Task<ActionResult<PaymentStatusDto>> Get(int id)
    {
        var PaymentStatus = await _mediator.Send(new GetPaymentStatusDetailRequest { PaymentStatusId = id });
        return Ok(PaymentStatus);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-paymentStatus")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePaymentStatusDto PaymentStatus)
    {
        var command = new CreatePaymentStatusCommand { PaymentStatusDto = PaymentStatus };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-paymentStatus/{id}")]
    public async Task<ActionResult> Put([FromBody] PaymentStatusDto PaymentStatus)
    {
        var command = new UpdatePaymentStatusCommand { PaymentStatusDto = PaymentStatus };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-paymentStatus/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeletePaymentStatusCommand { PaymentStatusId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedPaymentStatuss")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedPaymentStatus()
    {
        var selectedPaymentStatus = await _mediator.Send(new GetSelectedPaymentStatusRequest { });
        return Ok(selectedPaymentStatus);
    }

    [HttpGet]
    [Route("get-selectedPaymentStatusForCash")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedPaymentStatusForCash()
    {
        var selectedPaymentStatus = await _mediator.Send(new GetSelectedPaymentStatusForCashRequest { });
        return Ok(selectedPaymentStatus);
    }


}

