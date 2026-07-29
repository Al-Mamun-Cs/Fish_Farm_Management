using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.FishSales;
using SchoolManagement.Application.Features.FishSales.Requests.Commands;
using SchoolManagement.Application.Features.FishSales.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.FishSale)]
[ApiController]
[Authorize]
public class FishSaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public FishSaleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-FishSales")]
    public async Task<ActionResult<PagedResult<FishSaleDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var FishSales = await _mediator.Send(new GetFishSaleListRequest { QueryParams = queryParams, WarehouseId = warehouseId });
        return Ok(FishSales);
    }

    

    [HttpGet]
    [Route("get-FishSaleDetail/{id}")]
    public async Task<ActionResult<FishSaleDto>> Get(int id)
    {
        var FishSale = await _mediator.Send(new GetFishSaleDetailRequest { FishSaleId = id });
        return Ok(FishSale);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-FishSale")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFishSaleDto FishSale)
    {
        var command = new CreateFishSaleCommand { FishSaleDto = FishSale };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-FishSale/{id}")]
    public async Task<ActionResult> Put([FromBody] FishSaleDto FishSale)
    {
        var command = new UpdateFishSaleCommand { FishSaleDto = FishSale };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-FishSale/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteFishSaleCommand { FishSaleId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedFishSales")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedFishSale(int warehouseId)
    {
        var selectedFishSale = await _mediator.Send(new GetSelectedFishSaleRequest { WarehouseId = warehouseId });
        return Ok(selectedFishSale);
    }

    //[HttpGet]
    //[Route("get-SpGetTotalInvestor")]
    //public async Task<ActionResult> SpGetTotalInvestor(int warehouseId)
    //{
    //    var easyBikeListByType = await _mediator.Send(new SpGetTotalInvestorRequest
    //    {
    //        WarehouseId = warehouseId
    //    });
    //    return Ok(easyBikeListByType);
    //}

    //[HttpGet]
    //[Route("get-SpGetInvestorDetailList")]
    //public async Task<ActionResult> SpGetInvestorDetailList(int warehouseId)
    //{
    //    var easyBikeListByType = await _mediator.Send(new SpGetInvestorDetailListRequest
    //    {
    //        WarehouseId = warehouseId
    //    });
    //    return Ok(easyBikeListByType);
    //}


}

