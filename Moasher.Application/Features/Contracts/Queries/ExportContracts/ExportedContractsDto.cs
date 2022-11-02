using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Contracts.Queries.ExportContracts;

public record ExportedContractsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);