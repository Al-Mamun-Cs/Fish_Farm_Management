using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.CompanyInvestors;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Commands;
using SchoolManagement.Application.Features.CompanyInvestors.Requests.Queries;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.CompanyInvestor)]
[ApiController]
[Authorize]
public class CompanyInvestorController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyInvestorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-CompanyInvestors")]
    public async Task<ActionResult<PagedResult<CompanyInvestorDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var CompanyInvestors = await _mediator.Send(new GetCompanyInvestorListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(CompanyInvestors);
    }

    

    [HttpGet]
    [Route("get-CompanyInvestorDetail/{id}")]
    public async Task<ActionResult<CompanyInvestorDto>> Get(int id)
    {
        var CompanyInvestor = await _mediator.Send(new GetCompanyInvestorDetailRequest { CompanyInvestorId = id });
        return Ok(CompanyInvestor);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-CompanyInvestor")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCompanyInvestorDto CompanyInvestor)
    {
        var command = new CreateCompanyInvestorCommand { CompanyInvestorDto = CompanyInvestor };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-CompanyInvestor/{id}")]
    public async Task<ActionResult> Put([FromBody] CompanyInvestorDto CompanyInvestor)
    {
        var command = new UpdateCompanyInvestorCommand { CompanyInvestorDto = CompanyInvestor };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-CompanyInvestor/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteCompanyInvestorCommand { CompanyInvestorId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedCompanyInvestors")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedCompanyInvestor(int warehouseId)
    {
        var selectedCompanyInvestor = await _mediator.Send(new GetSelectedCompanyInvestorRequest { WarehouseId = warehouseId });
        return Ok(selectedCompanyInvestor);
    }

    [HttpGet]
    [Route("get-SpGetTotalInvestor")]
    public async Task<ActionResult> SpGetTotalInvestor(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalInvestorRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetInvestorDetailList")]
    public async Task<ActionResult> SpGetInvestorDetailList(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetInvestorDetailListRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }


}

