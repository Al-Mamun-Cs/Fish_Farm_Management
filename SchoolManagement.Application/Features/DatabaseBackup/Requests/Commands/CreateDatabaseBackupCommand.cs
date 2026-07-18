using MediatR;
using SchoolManagement.Application.DTOs.DatabaseBackups;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.DatabaseBackup.Requests.Commands
{
    public class CreateDatabaseBackupCommand : IRequest<BaseCommandResponse>
    {
        //public CreateDatabaseBackupDto DatabaseBackupDto { get; set; }
    }
}
