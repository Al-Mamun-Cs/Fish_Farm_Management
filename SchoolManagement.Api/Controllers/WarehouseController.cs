using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Warehouses;
using SchoolManagement.Application.Features.DailyMiscellaneousCosts.Requests.Queries;
using SchoolManagement.Application.Features.Warehouses.Requests.Commands;
using SchoolManagement.Application.Features.Warehouses.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Warehouse)]
[ApiController]
[Authorize]
public class WarehouseController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehouseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Warehouses")]
    public async Task<ActionResult<PagedResult<WarehouseDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Warehouses = await _mediator.Send(new GetWarehouseListRequest { QueryParams = queryParams });
        return Ok(Warehouses);
    }

    

    [HttpGet]
    [Route("get-WarehouseDetail/{id}")]
    public async Task<ActionResult<WarehouseDto>> Get(int id)
    {
        var Warehouse = await _mediator.Send(new GetWarehouseDetailRequest { WarehouseId = id });
        return Ok(Warehouse);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Warehouse")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateWarehouseDto Warehouse)
    {
        var command = new CreateWarehouseCommand { WarehouseDto = Warehouse };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Warehouse/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateWarehouseDto Warehouse)
    {
        var command = new UpdateWarehouseCommand { CreateWarehouseDto = Warehouse };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Warehouse/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteWarehouseCommand { WarehouseId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedWarehouses")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedWarehouse()
    {
        var selectedWarehouse = await _mediator.Send(new GetSelectedWarehouseRequest { });
        return Ok(selectedWarehouse);
    }

    [HttpGet]
    [Route("get-selectedWarehousesById")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedWarehouseById(int warehouseId)
    {
        var selectedWarehouse = await _mediator.Send(new GetSelectedWarehouseByIdRequest { WarehouseId = warehouseId });
        return Ok(selectedWarehouse);
    }

    [HttpGet]
    [Route("get-SpGetTotalCashCapital")]
    public async Task<ActionResult> SpGetTotalCashCapital(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalCashCapitalRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetCashCapitalDetail")]
    public async Task<ActionResult> SpGetCashCapitalDetail(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetCashCapitalDetailRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetTotalCashInHand")]
    public async Task<ActionResult> SpGetTotalCashInHand(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalCashInHandRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetCashInHandDetail")]
    public async Task<ActionResult> SpGetCashInHandDetail(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetCashInHandDetailRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }


}

