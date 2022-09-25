using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.KPIEntities;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Validators;

public class KPIValueDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<KPIValue> _kpiValues;
    private readonly short _year;
    private readonly TimePeriod _measurementPeriod;

    public KPIValueDomainValidator(List<KPIValue> kpiValues, short year, TimePeriod measurementPeriod)
    {
        _kpiValues = kpiValues;
        _year = year;
        _measurementPeriod = measurementPeriod;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        if (_kpiValues.Any(v => v.Year == _year && v.MeasurementPeriod == _measurementPeriod))
        {
            Errors[nameof(KPIValue.TargetValue)] = new[] {DomainValidationErrorMessages.Duplicated("المستهدف")};
        }

        return Errors;
    }
}