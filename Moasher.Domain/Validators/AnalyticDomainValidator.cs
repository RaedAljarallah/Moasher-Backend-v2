using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Validators;

public class AnalyticDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<Analytic> _analytics;
    private readonly DateTimeOffset _analyzedAt;

    public AnalyticDomainValidator(List<Analytic> analytics, DateTimeOffset analyzedAt)
    {
        _analytics = analytics;
        _analyzedAt = analyzedAt;
    }
    public IDictionary<string, string[]> Validate()
    {
        if (_analytics.Any(a => a.AnalyzedAt.Date == _analyzedAt))
        {
            Errors[nameof(Analytic.AnalyzedAt)] = new[] {DomainValidationErrorMessages.Duplicated("التحليل")};
        }
        return Errors;
    }
}