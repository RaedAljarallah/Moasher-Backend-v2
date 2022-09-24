using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Validators;

public class InitiativeTeamDomainValidator : DomainValidator, IDomainValidator
{
    private readonly Initiative _initiative;
    private readonly string _name;
    private readonly string _email;
    private readonly string _phone;
    private readonly Guid _roleEnumId;

    public InitiativeTeamDomainValidator(Initiative initiative, string name, string email, string phone, Guid roleEnumId)
    {
        _initiative = initiative;
        _name = name;
        _email = email;
        _phone = phone;
        _roleEnumId = roleEnumId;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var member in _initiative.Teams)
        {
            if (member.Name == _name && member.Phone == _phone && member.Email == _email &&
                member.RoleEnumId == _roleEnumId)
            {
                Errors[nameof(InitiativeMilestone.Name)] = new[] {DomainValidationErrorMessages.Duplicated("الاسم")};
            }
        }
        
        return Errors;
    }
}