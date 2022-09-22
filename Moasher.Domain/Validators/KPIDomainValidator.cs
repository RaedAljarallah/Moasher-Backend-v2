using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.KPIEntities;

namespace Moasher.Domain.Validators;

public class KPIDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<KPI> _kpis;
    private readonly string _name;
    private readonly string _code;

    public KPIDomainValidator(List<KPI> kpis, string name, string code)
    {
        _kpis = kpis;
        _name = name;
        _code = code;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var kpi in _kpis)
        {
            if (string.Equals(kpi.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(KPI.Name)] = new[] {DomainValidationErrorMessages.Duplicated("اسم المؤشر")};
            }

            if (string.Equals(kpi.Code, _code, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(KPI.Code)] = new[] {DomainValidationErrorMessages.Duplicated("رمز المؤشر")};
            }
            
        }

        return Errors;
    }
}