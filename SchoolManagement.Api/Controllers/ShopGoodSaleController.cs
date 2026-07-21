using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShopGoodSales;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Commands;
using SchoolManagement.Application.Features.ShopGoodSales.Requests.Queries;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShopGoodSale)]
[ApiController]
[Authorize]
public class ShopGoodSaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShopGoodSaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ShopGoodSales")]
    public async Task<ActionResult<PagedResult<ShopGoodSaleDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var ShopGoodSales = await _mediator.Send(new GetShopGoodSaleListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(ShopGoodSales);
    }

    

    [HttpGet]
    [Route("get-ShopGoodSaleDetail/{id}")]
    public async Task<ActionResult<ShopGoodSaleDto>> Get(int id)
    {
        var ShopGoodSale = await _mediator.Send(new GetShopGoodSaleDetailRequest { ShopGoodSaleId = id });
        return Ok(ShopGoodSale);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ShopGoodSale")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShopGoodSaleDetailDto ShopGoodSale)
    {
        var command = new CreateShopGoodSaleCommand { ShopGoodSaleDetailDto = ShopGoodSale };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ShopGoodSale/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShopGoodSaleCommand { ShopGoodSaleId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-ShopGoodSale/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveShopGoodSaleCommand { ShopGoodSaleId = id };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpGet]
    [Route("get-SpGetShopGoodSaleBillNo")]
    public async Task<ActionResult> SpGetShopGoodSaleBillNo()
    {
        var requisition = await _mediator.Send(new SpGetShopGoodSaleBillNoRequest
        {

        });
        return Ok(requisition);
    }

    [HttpGet]
    [Route("get-SpGetShopGoodSaleVoucherById")]
    public async Task<ActionResult> GetSpGetShopGoodSaleVoucherById(int shopGoodSaleId)
    {
        var getGoodSaleVoucherByGoodSaleId = await _mediator.Send(new SpGetShopGoodSaleVoucherByIdRequest
        {
            ShopGoodSaleId = shopGoodSaleId
        });
        return Ok(getGoodSaleVoucherByGoodSaleId);
    }

    [HttpGet]
    [Route("get-SpGetDailyTotalSalesAmount")]
    public async Task<ActionResult> SpGetDailyTotalSalesAmount(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetDailyTotalSalesAmountRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetDailySaleAmountList")]
    public async Task<ActionResult> SpGetDailySaleAmountList(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetDailySaleAmountListRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }



}

