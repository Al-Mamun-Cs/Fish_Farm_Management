using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Brands;
using SchoolManagement.Application.Features.Brands.Requests.Commands;
using SchoolManagement.Application.Features.Brands.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Brand)]
[ApiController]
[Authorize]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-Brands")]
    public async Task<ActionResult<PagedResult<BrandDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Brands = await _mediator.Send(new GetBrandListRequest { QueryParams = queryParams });
        return Ok(Brands);
    }

    

    [HttpGet]
    [Route("get-BrandDetail/{id}")]
    public async Task<ActionResult<BrandDto>> Get(int id)
    {
        var Brand = await _mediator.Send(new GetBrandDetailRequest { BrandId = id });
        return Ok(Brand);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-Brand")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateBrandDto Brand)
    {
        var command = new CreateBrandCommand { BrandDto = Brand };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-Brand/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateBrandDto Brand)
    {
        var command = new UpdateBrandCommand { CreateBrandDto = Brand };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-Brand/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteBrandCommand { BrandId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedBrands")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedBrand()
    {
        var selectedBrand = await _mediator.Send(new GetSelectedBrandRequest { });
        return Ok(selectedBrand);
    }

    
}

