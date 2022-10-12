using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Validators.Common.Extensions;

internal static class InitiativeChildrenDomainValidatorExtensions
{
    internal static void AchievedAfterInitiativeStart(this Initiative initiative, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish, string plannedPropertyName, string actualPropertyName,
        IDictionary<string, string[]> errorsContainer)
    {
        ApplyAfterInitiativeStartValidation(initiative, plannedFinish, actualFinish, "الإنجاز المخطط", "الإنجاز الفعلي",
            plannedPropertyName, actualPropertyName, errorsContainer);
    }

    internal static void BiddingAfterInitiativeStart(this Initiative initiative, DateTimeOffset plannedBidding,
        DateTimeOffset? actualBidding, string plannedPropertyName, string actualPropertyName,
        IDictionary<string, string[]> errorsContainer)
    {
        ApplyAfterInitiativeStartValidation(initiative, plannedBidding, actualBidding, "الطرح المخطط", "الطرح الفعلي",
            plannedPropertyName, actualPropertyName, errorsContainer);
    }

    internal static void ApprovedAfterInitiativeStart(this Initiative initiative, DateTimeOffset approvalDate,
        string propertyName, IDictionary<string, string[]> errorsContainer)
    {
        ApplyAfterInitiativeStartValidation(initiative, approvalDate, null, "اعتماد التكاليف", string.Empty,
            propertyName, propertyName, errorsContainer);
    }


    internal static void AchievedBeforeInitiativeFinish(this Initiative initiative, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish, string plannedPropertyName, string actualPropertyName,
        IDictionary<string, string[]> errorsContainer)
    {
        ApplyBeforeInitiativeFinishValidation(initiative, plannedFinish, actualFinish, "الإنجاز المخطط",
            "الإنجاز الفعلي",
            plannedPropertyName, actualPropertyName, errorsContainer);
    }

    internal static void ContractStartsAfterInitiativeStart(this Initiative initiative,
        DateTimeOffset contractStartDate, string propertyName, IDictionary<string, string[]> errorsContainer)
    {
        ApplyAfterInitiativeStartValidation(initiative, contractStartDate, null, "يداية العقد", string.Empty,
            propertyName, propertyName, errorsContainer);
    }
    
    internal static void BiddingBeforeInitiativeFinish(this Initiative initiative, DateTimeOffset plannedBidding,
        DateTimeOffset? actualBidding, string plannedPropertyName, string actualPropertyName,
        IDictionary<string, string[]> errorsContainer)
    {
        ApplyBeforeInitiativeFinishValidation(initiative, plannedBidding, actualBidding, "الطرح المخطط",
            "الطرح الفعلي",
            plannedPropertyName, actualPropertyName, errorsContainer);
    }

    internal static void ContractEndsBeforeInitiativeFinish(this Initiative initiative, DateTimeOffset contractEndDate,
        string propertyName, IDictionary<string, string[]> errorsContainer)
    {
        ApplyBeforeInitiativeFinishValidation(initiative, contractEndDate, null, "نهاية العقد", string.Empty,
            propertyName, propertyName, errorsContainer);
    }

    internal static void ApprovedBeforeInitiativeFinish(this Initiative initiative, DateTimeOffset approvalDate,
        string propertyName, IDictionary<string, string[]> errorsContainer)
    {
        ApplyBeforeInitiativeFinishValidation(initiative, approvalDate, null, "اعتماد التكاليق", string.Empty,
            propertyName, propertyName, errorsContainer);
    }

    private static void ApplyAfterInitiativeStartValidation(Initiative initiative, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish, string plannedMessageKey, string actualMessageKey, string plannedPropName,
        string actualPropName, IDictionary<string, string[]> errorsContainer)
    {
        if (initiative.ActualStart.HasValue)
        {
            if (plannedFinish < initiative.ActualStart)
            {
                errorsContainer[plannedPropName] = new[]
                {
                    $"تاريخ {plannedMessageKey} يجب أن يكون بعد تاريخ بداية المبادرة الفعلي [{initiative.ActualStart.Value:yyyy-MM-dd}]"
                };
            }

            if (actualFinish.HasValue && actualFinish < initiative.ActualStart)
            {
                errorsContainer[actualPropName] = new[]
                {
                    $"تاريخ {actualMessageKey} يجب أن يكون بعد تاريخ بداية المبادرة الفعلي [{initiative.ActualStart.Value:yyyy-MM-dd}]"
                };
            }
        }
        else
        {
            if (plannedFinish < initiative.PlannedStart)
            {
                errorsContainer[plannedPropName] = new[]
                {
                    $"تاريخ {plannedMessageKey} يجب أن يكون بعد تاريخ بداية المبادرة المخطط [{initiative.PlannedStart:yyyy-MM-dd}]"
                };
            }

            if (actualFinish.HasValue && actualFinish < initiative.PlannedStart)
            {
                errorsContainer[actualPropName] = new[]
                {
                    $"تاريخ {actualMessageKey} يجب أن يكون بعد تاريخ بداية المبادرة المخطط [{initiative.PlannedStart:yyyy-MM-dd}]"
                };
            }
        }
    }

    private static void ApplyBeforeInitiativeFinishValidation(Initiative initiative, DateTimeOffset plannedFinish,
        DateTimeOffset? actualFinish, string plannedMessageKey, string actualMessageKey, string plannedPropName,
        string actualPropName, IDictionary<string, string[]> errorsContainer)
    {
        if (initiative.ActualFinish.HasValue)
        {
            if (plannedFinish > initiative.ActualFinish)
            {
                errorsContainer[plannedPropName] = new[]
                {
                    $"تاريخ {plannedMessageKey} يجب أن يكون قبل تاريخ نهاية المبادرة الفعلي [{initiative.ActualFinish.Value:yyyy-MM-dd}]"
                };
            }

            if (actualFinish.HasValue && actualFinish > initiative.ActualFinish)
            {
                errorsContainer[actualPropName] = new[]
                {
                    $"تاريخ {actualMessageKey} يجب أن يكون قبل تاريخ نهاية المبادرة الفعلي [{initiative.ActualFinish.Value:yyyy-MM-dd}]"
                };
            }
        }
        else
        {
            if (plannedFinish > initiative.PlannedFinish)
            {
                errorsContainer[plannedPropName] = new[]
                {
                    $"تاريخ {plannedMessageKey} يجب أن يكون قبل تاريخ نهاية المبادرة المخطط [{initiative.PlannedFinish:yyyy-MM-dd}]"
                };
            }

            if (actualFinish.HasValue && actualFinish > initiative.PlannedFinish)
            {
                errorsContainer[actualPropName] = new[]
                {
                    $"تاريخ {actualMessageKey} يجب أن يكون قبل تاريخ نهاية المبادرة المخطط [{initiative.PlannedFinish:yyyy-MM-dd}]"
                };
            }
        }
    }
}