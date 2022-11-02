using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.KPIValues.Queries.ExportKPIsValues;

public record ExportedKPIsValuesDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);