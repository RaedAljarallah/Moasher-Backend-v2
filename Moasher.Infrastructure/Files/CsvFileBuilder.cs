using System.Globalization;
using CsvHelper;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Entities;
using Moasher.Infrastructure.Files.Maps;

namespace Moasher.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildEntitiesFile(IEnumerable<EntityDto> entities)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        
            csvWriter.Context.RegisterClassMap<EntityCsvMap>();
            csvWriter.WriteRecords(entities);
            
        }
        return memoryStream.ToArray();
    }
}