using AutoMapper;
using MediatR;
using Moasher.Application.Common.Interfaces;
using Moasher.Application.Features.Expenditures.Queries.GetExpenditures;

namespace Moasher.Application.Features.Expenditures.Queries.ExportExpenditures;

public record ExportExpendituresQuery : IRequest<ExportedExpendituresDto>;

public class ExportExpendituresQueryHandler : IRequestHandler<ExportExpendituresQuery, ExportedExpendituresDto>
{
    private readonly ISender _sender;
    private readonly ICsvFileBuilder _csvFileBuilder;

    public ExportExpendituresQueryHandler(ISender sender, ICsvFileBuilder csvFileBuilder)
    {
        _sender = sender;
        _csvFileBuilder = csvFileBuilder;
    }

    public async Task<ExportedExpendituresDto> Handle(ExportExpendituresQuery request, CancellationToken cancellationToken)
    {
        var expenditures = await _sender.Send(new GetExpendituresQuery(), cancellationToken);
        return new ExportedExpendituresDto("Expenditures.csv", _csvFileBuilder.BuildExpenditures(expenditures));
    }
}