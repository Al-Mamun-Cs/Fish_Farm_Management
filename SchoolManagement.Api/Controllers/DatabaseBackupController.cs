using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.Features.DatabaseBackup.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Application.DTOs.DatabaseBackups; // For response DTO

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")] // Or use SMSRoutePrefix
    [ApiController]
    public class DatabaseBackupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DatabaseBackupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("backup-database")] // Corresponds to SMSRoutePrefix.BackupDatabase
        public async Task<ActionResult<BaseCommandResponse>> BackupDatabase()
        {
            var command = new CreateDatabaseBackupCommand();
            var response = await _mediator.Send(command);
            // You might want to check response.Success and return appropriate status codes
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}