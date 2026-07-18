using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.DatabaseBackup.Requests.Commands;
using SchoolManagement.Application.Responses;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.DatabaseBackup.Handlers.Commands
{
    public class CreateDatabaseBackupCommandHandler : IRequestHandler<CreateDatabaseBackupCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateDatabaseBackupCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseCommandResponse> Handle(CreateDatabaseBackupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var backupSettings = _configuration.GetSection("DatabaseBackup");
                var backupPath = backupSettings["SqlServerPath"];
                var dbName = backupSettings["SqlServerDbName"];

                if (string.IsNullOrEmpty(backupPath) || string.IsNullOrEmpty(dbName))
                {
                    response.Success = false;
                    response.Message = "Backup path or database name is not configured.";
                    return response;
                }

                // Create directory if not exists
                Directory.CreateDirectory(backupPath);

                var timestamp = DateTime.Now.ToString("ddMMMyyyy_HH-mm");
                var backupFileName = $"{dbName}_backup_{timestamp}.bak";
                var fullBackupPath = Path.Combine(backupPath, backupFileName);

                var backupCommand = $@"
                    BACKUP DATABASE [{dbName}]
                    TO DISK = N'{fullBackupPath}'
                    WITH NOFORMAT, NOINIT,
                    NAME = N'{dbName} Full Backup {timestamp}', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

                // Use repository method to execute backup
                await _unitOfWork.Repository<object>().ExecuteSqlRawAsync(backupCommand);

                response.Success = true;
                response.Message = $"Database backup created successfully at: {fullBackupPath}";
                response.Id = 0;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Database backup failed: {ex.Message}";
            }

            return response;
        }
    }
}
