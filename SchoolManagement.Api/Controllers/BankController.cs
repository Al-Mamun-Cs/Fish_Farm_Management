using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Banks;
using SchoolManagement.Application.Features.Banks.Requests.Commands;
using SchoolManagement.Application.Features.Banks.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Bank)]
[ApiController]
[Authorize]
public class BankController : ControllerBase
{
    private readonly IMediator _mediator;

    public BankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Banks")]
    public async Task<ActionResult<PagedResult<BankDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Banks = await _mediator.Send(new GetBankListRequest { QueryParams = queryParams });
        return Ok(Banks);
    }


    [HttpGet]
    [Route("get-BankDetail/{id}")]
    public async Task<ActionResult<BankDto>> Get(int id)
    {
        var Bank = await _mediator.Send(new GetBankDetailRequest { BankId = id });
        return Ok(Bank);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Bank")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBankDto Bank)
    {
        var command = new CreateBankCommand { BankDto = Bank };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Bank/{id}")]
    public async Task<ActionResult> Put([FromBody] BankDto Bank)
    {
        var command = new UpdateBankCommand { BankDto = Bank };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Bank/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBankCommand { BankId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedBanks")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedBank()
    {
        var selectedBank = await _mediator.Send(new GetSelectedBankRequest { });
        return Ok(selectedBank);
    }

    
}

