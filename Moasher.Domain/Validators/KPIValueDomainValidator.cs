using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Validators;

public class KPIValueDomainValidator : DomainValidator, IDomainValidator
{
    private readonly KPI _kpi;
    private readonly List<KPIValue> _kpiValues;
    private readonly short _year;
    private readonly TimePeriod _measurementPeriod;
    private readonly DateTimeOffset _plannedFinish;
    private readonly DateTimeOffset? _actualFinish;

    public KPIValueDomainValidator(KPI kpi, List<KPIValue> kpiValues, short year, TimePeriod measurementPeriod,
        DateTimeOffset plannedFinish, DateTimeOffset? actualFinish)
    {
        _kpi = kpi;
        _kpiValues = kpiValues;
        _year = year;
        _measurementPeriod = measurementPeriod;
        _plannedFinish = plannedFinish;
        _actualFinish = actualFinish;
    }

    public IDictionary<string, string[]> Validate()
    {
        if (_kpiValues.Any(v => v.Year == _year && v.MeasurementPeriod == _measurementPeriod))
        {
            Errors[nameof(KPIValue.TargetValue)] = new[] {DomainValidationErrorMessages.Duplicated("المستهدف")};
        }

        if (_plannedFinish < _kpi.StartDate)
        {
            Errors[nameof(KPIValue.PlannedFinish)] = new[]
                {$"تاريخ الإنجاز المخطط يجب أن يكون بعد تاريخ بداية قياس المؤشر [{_kpi.StartDate:yyyy-MM-dd}]"};
        }

        if (_actualFinish > _kpi.EndDate)
        {
            Errors[nameof(KPIValue.ActualFinish)] = new[]
                {$"تاريخ الإنجاز الفعلي يجب أن يكون قبل تاريخ نهاية قياس المؤشر [{_kpi.EndDate:yyyy-MM-dd}]"};
        }
        return Errors;
    }
}