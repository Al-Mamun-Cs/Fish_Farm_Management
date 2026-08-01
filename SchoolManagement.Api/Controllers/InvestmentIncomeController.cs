using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.InvestmentIncomes;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Commands;
using SchoolManagement.Application.Features.InvestmentIncomes.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.InvestmentIncome)]
[ApiController]
[Authorize]
public class InvestmentIncomeController : ControllerBase
{
    private readonly IMediator _mediator;

    public InvestmentIncomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-InvestmentIncomes")]
    public async Task<ActionResult<PagedResult<InvestmentIncomeDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var InvestmentIncomes = await _mediator.Send(new GetInvestmentIncomeListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(InvestmentIncomes);
    }


    [HttpGet]
    [Route("get-InvestmentIncomeDetail/{id}")]
    public async Task<ActionResult<InvestmentIncomeDto>> Get(int id)
    {
        var InvestmentIncome = await _mediator.Send(new GetInvestmentIncomeDetailRequest { InvestmentIncomeId = id });
        return Ok(InvestmentIncome);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-InvestmentIncome")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateInvestmentIncomeDto InvestmentIncome)
    {
        var command = new CreateInvestmentIncomeCommand { InvestmentIncomeDto = InvestmentIncome };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-InvestmentIncome/{id}")]
    public async Task<ActionResult> Put([FromBody] InvestmentIncomeDto InvestmentIncome)
    {
        var command = new UpdateInvestmentIncomeCommand { InvestmentIncomeDto = InvestmentIncome };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-InvestmentIncome/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteInvestmentIncomeCommand { InvestmentIncomeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedInvestmentIncomes")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedInvestmentIncome()
    {
        var selectedInvestmentIncome = await _mediator.Send(new GetSelectedInvestmentIncomeRequest { });
        return Ok(selectedInvestmentIncome);
    }

   

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-InvestmentIncome/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveInvestmentIncomeCommand { InvestmentIncomeId = id };
        await _mediator.Send(command);
        return NoContent();
    }


}

