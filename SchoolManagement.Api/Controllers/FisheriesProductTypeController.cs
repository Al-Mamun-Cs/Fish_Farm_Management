using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FisheriesProductTypes;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Commands;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FisheriesProductType)]
[ApiController]
[Authorize]
public class FisheriesProductTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public FisheriesProductTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-FisheriesProductTypes")]
    public async Task<ActionResult<PagedResult<FisheriesProductTypeDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var FisheriesProductTypes = await _mediator.Send(new GetFisheriesProductTypeListRequest { QueryParams = queryParams,WarehouseId = warehouseId });
        return Ok(FisheriesProductTypes);
    }


    [HttpGet]
    [Route("get-FisheriesProductTypeDetail/{id}")]
    public async Task<ActionResult<FisheriesProductTypeDto>> Get(int id)
    {
        var FisheriesProductType = await _mediator.Send(new GetFisheriesProductTypeDetailRequest { FisheriesProductTypeId = id });
        return Ok(FisheriesProductType);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FisheriesProductType")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFisheriesProductTypeDto FisheriesProductType)
    {
        var command = new CreateFisheriesProductTypeCommand { FisheriesProductTypeDto = FisheriesProductType };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FisheriesProductType/{id}")]
    public async Task<ActionResult> Put([FromBody] FisheriesProductTypeDto FisheriesProductType)
    {
        var command = new UpdateFisheriesProductTypeCommand { FisheriesProductTypeDto = FisheriesProductType };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FisheriesProductType/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFisheriesProductTypeCommand { FisheriesProductTypeId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedFisheriesProductTypes")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedFisheriesProductType(int warehouseId)
    {
        var selectedFisheriesProductType = await _mediator.Send(new GetSelectedFisheriesProductTypeRequest {WarehouseId = warehouseId });
        return Ok(selectedFisheriesProductType);
    }

    [HttpGet]
    [Route("get-SpGetTotalFisheriesProductTypeList")]
    public async Task<ActionResult> SpGetTotalFisheriesProductTypeList( int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalFisheriesProductTypeListRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetFisheriesProductStockByIdList")]
    public async Task<ActionResult> SpGetFisheriesProductStockById(int warehouseId, int fisheriesProductTypeId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetFisheriesProductStockByIdListRequest
        {
            WarehouseId = warehouseId,
            FisheriesProductTypeId = fisheriesProductTypeId
        });
        return Ok(easyBikeListByType);
    }


}

