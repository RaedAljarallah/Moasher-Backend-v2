using Moasher.Application.Common.Abstracts;

namespace Moasher.Application.Features.Budgets.Queries.ExportBudgets;

public record ExportedBudgetsDto(string FileName, byte[] Content) : ExportedCsvFileBase(FileName, Content);