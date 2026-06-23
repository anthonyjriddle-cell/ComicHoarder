using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace ComicHoarder.Infrastructure.Logging
{
    public static class EntityChangeLogger
    {
        public static void LogChanges(
            EntityEntry entry,
            string entityName,
            ILogger logger)
        {
            foreach (var prop in entry.Properties)
            {
                if (!prop.IsModified)
                    continue;

                var field = prop.Metadata.Name;
                var oldValue = prop.OriginalValue;
                var newValue = prop.CurrentValue;

                logger.LogInformation(
                    $"{entityName}.{field} changed | {Format(oldValue)} | {Format(newValue)}"
                );
            }
        }

        private static string Format(object? value)
        {
            return value == null ? "null" : value.ToString()!;
        }
    }
}
