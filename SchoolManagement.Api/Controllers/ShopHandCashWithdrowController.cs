using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShopHandCashWithdrows;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Commands;
using SchoolManagement.Application.Features.ShopHandCashWithdrows.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShopHandCashWithdrow)]
[ApiController]
[Authorize]
public class ShopHandCashWithdrowController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShopHandCashWithdrowController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ShopHandCashWithdrows")]
    public async Task<ActionResult<PagedResult<ShopHandCashWithdrowDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var ShopHandCashWithdrows = await _mediator.Send(new GetShopHandCashWithdrowListRequest { QueryParams = queryParams });
        return Ok(ShopHandCashWithdrows);
    }

    [HttpGet]
    [Route("get-Investments")]
    public async Task<ActionResult<PagedResult<ShopHandCashWithdrowDto>>> GetInvestment([FromQuery] QueryParams queryParams)
    {
        var ShopHandCashWithdrows = await _mediator.Send(new GetInvestmentListRequest { QueryParams = queryParams });
        return Ok(ShopHandCashWithdrows);
    }


    [HttpGet]
    [Route("get-ShopHandCashWithdrowDetail/{id}")]
    public async Task<ActionResult<ShopHandCashWithdrowDto>> Get(int id)
    {
        var ShopHandCashWithdrow = await _mediator.Send(new GetShopHandCashWithdrowDetailRequest { ShopHandCashWithdrowId = id });
        return Ok(ShopHandCashWithdrow);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ShopHandCashWithdrow")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShopHandCashWithdrowDto ShopHandCashWithdrow)
    {
        var command = new CreateShopHandCashWithdrowCommand { ShopHandCashWithdrowDto = ShopHandCashWithdrow };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-ShopHandCashWithdrow/{id}")]
    public async Task<ActionResult> Put([FromBody] ShopHandCashWithdrowDto ShopHandCashWithdrow)
    {
        var command = new UpdateShopHandCashWithdrowCommand { ShopHandCashWithdrowDto = ShopHandCashWithdrow };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ShopHandCashWithdrow/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShopHandCashWithdrowCommand { ShopHandCashWithdrowId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedShopHandCashWithdrows")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedShopHandCashWithdrow()
    {
        var selectedShopHandCashWithdrow = await _mediator.Send(new GetSelectedShopHandCashWithdrowRequest { });
        return Ok(selectedShopHandCashWithdrow);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-ShopHandCashWithdrow/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveShopHandCashWithdrowCommand { ShopHandCashWithdrowId = id };
        await _mediator.Send(command);
        return NoContent();
    }


}

