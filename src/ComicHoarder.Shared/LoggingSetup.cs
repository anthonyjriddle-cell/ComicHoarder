using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace ComicHoarder.Shared
{
    public static class LoggingSetup
    {
        public static ILoggerFactory CreateLoggerFactory(IConfiguration configuration, string appName)
        {
            var connectionString = configuration["Logging:ConnectionString"] ?? string.Empty;
            var tableName = configuration["Logging:TableName"] ?? "Logs";
            var schemaName = configuration["Logging:SchemaName"] ?? "logs";

            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "Logger",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 128,
                    AllowNull = false
                }
            };

            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("Logger", appName)
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.ff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.MSSqlServer(
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    connectionString: connectionString,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = tableName,
                        SchemaName = schemaName,
                        AutoCreateSqlTable = true
                    },
                    columnOptions: columnOptions)
                .CreateLogger();

            return LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(serilogLogger, dispose: true);
            });
        }
    }
}