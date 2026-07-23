using SchoolManagement.Application;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Application.Features.FisheriesProductTypes.Requests.Queries;
using SchoolManagement.Application.Features.Suppliers.Requests.Commands;
using SchoolManagement.Application.Features.Suppliers.Requests.Queries;
using SchoolManagement.Application.Models;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Api.Controllers;

[Route(SMSRoutePrefix.Supplier)]
[ApiController]
[Authorize]
public class SupplierController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-suppliers")]
    public async Task<ActionResult<PagedResult<SupplierDto>>> Get([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var Suppliers = await _mediator.Send(new GetSupplierListRequest 
        { 
            QueryParams = queryParams,
            WarehouseId = warehouseId
        });
        return Ok(Suppliers);
    }

    [HttpGet]
    [Route("get-suppliersByStatus")]
    public async Task<ActionResult<PagedResult<SupplierDto>>> GetSuppliers([FromQuery] QueryParams queryParams, int warehouseId, int supplierStatus)
    {
        var Suppliers = await _mediator.Send(new GetSupplierListByStatusRequest
        {
            QueryParams = queryParams,
            WarehouseId = warehouseId,
            SupplierStatus = supplierStatus
        });
        return Ok(Suppliers);
    }

    [HttpGet]
    [Route("get-suppliersForBikroy")]
    public async Task<ActionResult<PagedResult<SupplierDto>>> GetForBikroy([FromQuery] QueryParams queryParams, int warehouseId)
    {
        var Suppliers = await _mediator.Send(new GetSupplierListForBikroyRequest 
        { 
            QueryParams = queryParams,
            WarehouseId = warehouseId
        });
        return Ok(Suppliers);
    }


    [HttpGet]
    [Route("get-supplierDetail/{id}")]
    public async Task<ActionResult<SupplierDto>> Get(int id)
    {
        var Supplier = await _mediator.Send(new GetSupplierDetailRequest { SupplierId = id });
        return Ok(Supplier);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-supplier")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromForm] CreateSupplierDto Supplier)
    {
        var command = new CreateSupplierCommand { SupplierDto = Supplier };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-supplier/{id}")]
    public async Task<ActionResult> Put([FromForm] CreateSupplierDto Supplier)
    {
        var command = new UpdateSupplierCommand { CreateSupplierDto = Supplier };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-supplier/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteSupplierCommand { SupplierId = id };
        await _mediator.Send(command);
        return NoContent();
    }

    // relational data get 

    [HttpGet]
    [Route("get-selectedSuppliers")]
    public async Task<ActionResult<List<SelectedModel>>> getselectedSupplier(int warehouseId,int supplierStatus)
    {
        var selectedSupplier = await _mediator.Send(new GetSelectedSupplierRequest 
        {
            WarehouseId = warehouseId,
            SupplierStatus = supplierStatus
        });
        return Ok(selectedSupplier);
    }

    [HttpGet]
    [Route("get-selectedSupplierByWarehouseId")]
    public async Task<ActionResult<List<SupplierDto>>> selectedSupplierByWarehouseId(int warehouseId)
    {
        var selectedSupplier = await _mediator.Send(new GetSelectedSupplierByWarehouseIdRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(selectedSupplier);
    }
    [HttpGet]
    [Route("get-selectedSupplierByWarehouseDueId")]
    public async Task<ActionResult<List<SupplierDto>>> selectedSupplierByWarehouseDueId(int warehouseId)
    {
        var selectedSupplier = await _mediator.Send(new GetSelectedSupplierByWarehouseIdDueRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(selectedSupplier);
    }

    [HttpGet]
    [Route("get-selectedSupplierByWarehouseIdForBikroy")]
    public async Task<ActionResult<List<SupplierDto>>> selectedSupplierByWarehouseIdForBikroy(int warehouseId)
    {
        var selectedSupplier = await _mediator.Send(new GetSelectedSupplierByWarehouseIdForBikroyRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(selectedSupplier);
    }

    [HttpGet]
    [Route("get-selectedSupplierByWarehouseIdForKroy")]
    public async Task<ActionResult<List<SupplierDto>>> selectedSupplierByWarehouseIdForKroy(int warehouseId)
    {
        var selectedSupplier = await _mediator.Send(new GetSelectedSupplierByWarehouseIdForKroyRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(selectedSupplier);
    }

    [HttpGet]
    [Route("get-phoneNoIsExistCheck")]
    public async Task<ActionResult<bool>> GetPhoneNoIsEXistCheck(string phoneNo)
    {
        var isExist = await _mediator.Send(new GetPhoneNoIsExistCheckRequest
        {
            PhoneNo = phoneNo,
        });
        return Ok(isExist);
    }

    [HttpGet]
    [Route("get-AutoCompleteSupplierName")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteVoucerNo(string supplierName,int warehouseId)
    {
        var course = await _mediator.Send(new GetAutoCompleteSupplierNameRequest
        {
            SupplierName = supplierName,
            WarehouseId = warehouseId,
        });
        return Ok(course);
    }

   

    [HttpGet]
    [Route("get-AutoCompleteForBankTransaction")]
    public async Task<ActionResult<List<SelectedModel>>> GetAutoCompleteForBankTransaction(string supplierName, int warehouseId)
    {
        var course = await _mediator.Send(new GetAutoCompleteForBankTransactionRequest
        {
            SupplierName = supplierName,
            WarehouseId = warehouseId,
        });
        return Ok(course);
    }

    [HttpGet]
    [Route("get-SupplierDetailById")]
    public async Task<ActionResult> GetSupplierById(int supplierId)
    {
        var requisitionDetailById = await _mediator.Send(new GetSpGetSupplierByIdRequest
        {
            SupplierId = supplierId
        });
        return Ok(requisitionDetailById);
    }

   

    [HttpGet]
    [Route("get-SpGetTotalSupplierDueAmount")]
    public async Task<ActionResult> SpGetTotalSupplierDueAmount(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalSupplierDueAmountRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetTotalDueAmountList")]
    public async Task<ActionResult> SpGetTotalDueAmountList(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalDueAmountListRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetTotalCustomerDueAmount")]
    public async Task<ActionResult> SpGetTotalCustomerDueAmount(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalCustomerDueAmountRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }

    [HttpGet]
    [Route("get-SpGetTotalCustomerDueAmountList")]
    public async Task<ActionResult> SpGetTotalCustomerDueAmountList(int warehouseId)
    {
        var easyBikeListByType = await _mediator.Send(new SpGetTotalCustomerDueAmountListRequest
        {
            WarehouseId = warehouseId
        });
        return Ok(easyBikeListByType);
    }
}

