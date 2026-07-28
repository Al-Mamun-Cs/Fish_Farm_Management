using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.CompanyInvestorReturns;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Commands;
using SchoolManagement.Application.Features.CompanyInvestorReturns.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.CompanyInvestorReturn)]
[ApiController]
[Authorize]
public class CompanyInvestorReturnController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyInvestorReturnController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-CompanyInvestorReturns")]
    public async Task<ActionResult<PagedResult<CompanyInvestorReturnDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var CompanyInvestorReturns = await _mediator.Send(new GetCompanyInvestorReturnListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(CompanyInvestorReturns);
    }


    [HttpGet]
    [Route("get-CompanyInvestorReturnDetail/{id}")]
    public async Task<ActionResult<CompanyInvestorReturnDto>> Get(int id)
    {
        var CompanyInvestorReturn = await _mediator.Send(new GetCompanyInvestorReturnDetailRequest { CompanyInvestorReturnId = id });
        return Ok(CompanyInvestorReturn);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-CompanyInvestorReturn")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCompanyInvestorReturnDto CompanyInvestorReturn)
    {
        var command = new CreateCompanyInvestorReturnCommand { CompanyInvestorReturnDto = CompanyInvestorReturn };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-CompanyInvestorReturn/{id}")]
    public async Task<ActionResult> Put([FromBody] CompanyInvestorReturnDto CompanyInvestorReturn)
    {
        var command = new UpdateCompanyInvestorReturnCommand { CompanyInvestorReturnDto = CompanyInvestorReturn };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-CompanyInvestorReturn/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCompanyInvestorReturnCommand { CompanyInvestorReturnId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedCompanyInvestorReturns")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedCompanyInvestorReturn()
    {
        var selectedCompanyInvestorReturn = await _mediator.Send(new GetSelectedCompanyInvestorReturnRequest { });
        return Ok(selectedCompanyInvestorReturn);
    }

   

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-CompanyInvestorReturn/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveCompanyInvestorReturnCommand { CompanyInvestorReturnId = id };
        await _mediator.Send(command);
        return NoContent();
    }


}

