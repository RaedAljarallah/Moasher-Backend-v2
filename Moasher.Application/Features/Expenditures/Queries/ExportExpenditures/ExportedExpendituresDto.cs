using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Expenditures.Queries.ExportExpenditures;

public record ExportedExpendituresDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);