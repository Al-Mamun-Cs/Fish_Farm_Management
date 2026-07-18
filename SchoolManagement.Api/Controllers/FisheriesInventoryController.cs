using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FisheriesInventorys;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Commands;
using SchoolManagement.Application.Features.FisheriesInventorys.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FisheriesInventory)]
[ApiController]
[Authorize]
public class FisheriesInventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public FisheriesInventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-FisheriesInventorys")]
    public async Task<ActionResult<PagedResult<FisheriesInventoryDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var FisheriesInventorys = await _mediator.Send(new GetFisheriesInventoryListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(FisheriesInventorys);
    }

    

    [HttpGet]
    [Route("get-FisheriesInventoryDetail/{id}")]
    public async Task<ActionResult<FisheriesInventoryDto>> Get(int id)
    {
        var FisheriesInventory = await _mediator.Send(new GetFisheriesInventoryDetailRequest { FisheriesInventoryId = id });
        return Ok(FisheriesInventory);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FisheriesInventory")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFisheriesInventoryDetailDto FisheriesInventory)
    {
        var command = new CreateFisheriesInventoryCommand { FisheriesInventoryDetailDto = FisheriesInventory };
        var response = await _mediator.Send(command);
        return Ok(response);
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FisheriesInventory/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFisheriesInventoryCommand { FisheriesInventoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("inActive-FisheriesInventory/{id}")]
    public async Task<ActionResult> RequisitionInActive(int id)
    {
        var command = new InActiveFisheriesInventoryCommand { FisheriesInventoryId = id };
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpGet]
    [Route("get-SpGetFisheriesBillNo")]
    public async Task<ActionResult> SpGetPurchaseBillNo()
    {
        var requisition = await _mediator.Send(new SpGetFisheriesBillNoRequest
        {

        });
        return Ok(requisition);
    }

    [HttpGet]
    [Route("get-SpGetFisheriesInventoryVoucherById")]
    public async Task<ActionResult> GetSpGetFisheriesInventoryVoucherById(int fisheriesInventoryId)
    {
        var getGoodSaleVoucherByGoodSaleId = await _mediator.Send(new SpGetFisheriesInventoryVoucherByIdRequest
        {
            FisheriesInventoryId = fisheriesInventoryId
        });
        return Ok(getGoodSaleVoucherByGoodSaleId);
    }

    [HttpGet]
    [Route("get-AutoCompleteProductName")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteProductName(string productName, int warehouseId,int fisheriesProductTypeId)
    {
        var course = await _mediator.Send(new GetAutoCompleteProductNameRequest
        {
            ProductName = productName,
            WarehouseId = warehouseId,
            FisheriesProductTypeId = fisheriesProductTypeId
        });
        return Ok(course);
    }


}

