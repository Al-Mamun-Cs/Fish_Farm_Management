using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.ShopInventorys;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Features.ShopInventoryDetails.Requests.Queries;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Commands;
using SchoolManagement.Application.Features.ShopInventorys.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.ShopInventory)]
[ApiController]
[Authorize]
public class ShopInventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShopInventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-ShopInventorys")]
    public async Task<ActionResult<PagedResult<ShopInventoryDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var ShopInventorys = await _mediator.Send(new GetShopInventoryListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(ShopInventorys);
    }

    

    [HttpGet]
    [Route("get-ShopInventoryDetail/{id}")]
    public async Task<ActionResult<ShopInventoryDto>> Get(int id)
    {
        var ShopInventory = await _mediator.Send(new GetShopInventoryDetailRequest { ShopInventoryId = id });
        return Ok(ShopInventory);
    }

    [HttpGet]
    [Route("get-ShopDetail/{id}")]
    public async Task<ActionResult<ShopInventoryDto>> GetShopDetail(int id)
    {
        var ShopInventory = await _mediator.Send(new GetShopDetailRequest { ShopInventoryDetailId = id });
        return Ok(ShopInventory);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-ShopInventory")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateShopInventoryDetailDto ShopInventory)
    {
        var command = new CreateShopInventoryCommand { ShopInventoryDetailDto = ShopInventory };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-ShopInventory/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteShopInventoryCommand { ShopInventoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-ShopInventory/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveShopInventoryCommand { ShopInventoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpGet]
    [Route("get-SpGetShopInventoryBillNo")]
    public async Task<ActionResult> SpGetShopInventoryBillNo()
    {
        var requisition = await _mediator.Send(new SpGetShopInventoryBillNoRequest
        {

        });
        return Ok(requisition);
    }

    [HttpGet]
    [Route("get-SpGetShopInventoryVoucherById")]
    public async Task<ActionResult> GetSpGetShopInventoryVoucherById(int shopInventoryId)
    {
        var getGoodSaleVoucherByGoodSaleId = await _mediator.Send(new SpGetShopInventoryVoucherByIdRequest
        {
            ShopInventoryId = shopInventoryId
        });
        return Ok(getGoodSaleVoucherByGoodSaleId);
    }

    [HttpGet]
    [Route("get-selectedShopInventoryProductName")]
    public async Task<ActionResult<List<SelectedModel>>> getShopInventoryProductName(int warehouseId,int fisheriesProductTypeId)
    {
        var selectedFisheriesProductType = await _mediator.Send(new GetSelectedShopInventoryProductNameRequest 
        { 
            WarehouseId = warehouseId,
            FisheriesProductTypeId= fisheriesProductTypeId
        });
        return Ok(selectedFisheriesProductType);
    }


}

