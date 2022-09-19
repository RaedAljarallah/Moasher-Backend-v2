using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Validators;

public class MilestoneDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _name;
    private readonly float _weight;
    private readonly DateTimeOffset _plannedFinish;
    private readonly DateTimeOffset? _actualFinish;

    public MilestoneDomainValidator(Initiative initiative, string name, float weight, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish)
    {
        _initiative = initiative;
        _name = name;
        _weight = weight;
        _plannedFinish = plannedFinish;
        _actualFinish = actualFinish;
    }

    public IDictionary<string, string[]> Validate()
    {
        foreach (var milestone in _initiative.Milestones)
        {
            if (milestone.Name == _name)
            {
                Errors["Name"] = new[] {DomainValidationErrorMessages.Duplicated("اسم المعلم")};
            }
        }

        var weightSum = _initiative.Milestones.Sum(m => m.Weight);
        if (weightSum + _weight > 100)
        {
            Errors["Weight"] = new[] {$"مجموع اوزان المعالم يجب أن لا تزيد عن 100 - الوزن المتبقي [{100 - weightSum}]"};
        }

        if (_initiative.ActualStart.HasValue)
        {
            if (_plannedFinish < _initiative.ActualStart)
            {
                Errors["PlannedFinish"] = new[]
                {
                    $"تاريخ الإنجاز المخطط يجب أن يكون بعد تاريخ بداية المبادرة الفعلي [{_initiative.ActualStart.Value:yyyy-MM-dd}]"
                };
            }

            if (_actualFinish < _initiative.ActualStart)
            {
                Errors["ActualFinish"] = new[]
                {
                    $"تاريخ الإنجاز الفعلي يجب أن يكون بعد تاريخ بداية المبادرة الفعلي [{_initiative.ActualStart.Value:yyyy-MM-dd}]"
                };
            }
        }
        else
        {
            if (_plannedFinish < _initiative.PlannedStart)
            {
                Errors["PlannedFinish"] = new[]
                {
                    $"تاريخ الإنجاز المخطط يجب أن يكون بعد تاريخ بداية المبادرة المخطط [{_initiative.PlannedStart:yyyy-MM-dd}]"
                };
            }
            
            if (_actualFinish < _initiative.PlannedStart)
            {
                Errors["ActualFinish"] = new[]
                {
                    $"تاريخ الإنجاز الفعلي يجب أن يكون بعد تاريخ بداية المبادرة المخطط [{_initiative.PlannedStart:yyyy-MM-dd}]"
                };
            }
        }

        if (_initiative.ActualFinish.HasValue)
        {
            if (_plannedFinish > _initiative.ActualFinish)
            {
                Errors["PlannedFinish"] = new[]
                {
                    $"تاريخ الإنجاز المخطط يجب أن يكون قبل تاريخ نهاية المبادرة الفعلي [{_initiative.ActualFinish.Value:yyyy-MM-dd}]"
                };
            }
            
            if (_actualFinish > _initiative.ActualFinish)
            {
                Errors["ActualFinish"] = new[]
                {
                    $"تاريخ الإنجاز الفعلي يجب أن يكون قبل تاريخ نهاية المبادرة الفعلي [{_initiative.ActualFinish.Value:yyyy-MM-dd}]"
                };
            }
        }
        else
        {
            if (_plannedFinish < _initiative.PlannedFinish)
            {
                Errors["PlannedFinish"] = new[]
                {
                    $"تاريخ الإنجاز المخطط يجب أن يكون قبل تاريخ نهاية المبادرة المخطط [{_initiative.PlannedFinish:yyyy-MM-dd}]"
                };
            }
            
            if (_actualFinish < _initiative.PlannedFinish)
            {
                Errors["ActualFinish"] = new[]
                {
                    $"تاريخ الإنجاز الفعلي يجب أن يكون قبل تاريخ نهاية المبادرة المخطط [{_initiative.PlannedFinish:yyyy-MM-dd}]"
                };
            }
        }
        
        
        return Errors;
    }
}