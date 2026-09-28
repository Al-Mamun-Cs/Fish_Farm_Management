using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.EasyBikeBanks;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Commands;
using SchoolManagement.Application.Features.EasyBikeBanks.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.EasyBikeBank)]
[ApiController]
[Authorize]
public class EasyBikeBankController : ControllerBase
{
    private readonly IMediator _mediator;

    public EasyBikeBankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-EasyBikeBanks")]
    public async Task<ActionResult<PagedResult<EasyBikeBankDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var EasyBikeBanks = await _mediator.Send(new GetEasyBikeBankListRequest { QueryParams = queryParams });
        return Ok(EasyBikeBanks);
    }

    

    [HttpGet]
    [Route("get-EasyBikeBankDetail/{id}")]
    public async Task<ActionResult<EasyBikeBankDto>> Get(int id)
    {
        var EasyBikeBank = await _mediator.Send(new GetEasyBikeBankDetailRequest { EasyBikeBankId = id });
        return Ok(EasyBikeBank);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-EasyBikeBank")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateEasyBikeBankDto EasyBikeBank)
    {
        var command = new CreateEasyBikeBankCommand { EasyBikeBankDto = EasyBikeBank };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-EasyBikeBank/{id}")]
    public async Task<ActionResult> Put([FromBody] EasyBikeBankDto EasyBikeBank)
    {
        var command = new UpdateEasyBikeBankCommand { EasyBikeBankDto = EasyBikeBank };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-EasyBikeBank/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteEasyBikeBankCommand { EasyBikeBankId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedEasyBikeBanks")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedEasyBikeBank()
    {
        var selectedEasyBikeBank = await _mediator.Send(new GetSelectedEasyBikeBankRequest { });
        return Ok(selectedEasyBikeBank);
    }
    [HttpGet]
    [Route("get-selectedEasyBikeBankNames")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedEasyBikeBankNames()
    {
        var selectedEasyBikeBank = await _mediator.Send(new GetSelectedEasyBikeBankNamesRequest { });
        return Ok(selectedEasyBikeBank);
    }
}

