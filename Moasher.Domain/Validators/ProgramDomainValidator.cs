using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Validators;

public class ProgramDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<Program> _programs;
    private readonly string _name;
    private readonly string _code;

    public ProgramDomainValidator(List<Program> programs, string name, string code)
    {
        _programs = programs;
        _name = name;
        _code = code;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var program in _programs)
        {
            if (string.Equals(program.Name, _name, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(Program.Name)] = new[] {DomainValidationErrorMessages.Duplicated("اسم البرنامج")};
            }

            if (string.Equals(program.Code, _code, StringComparison.CurrentCultureIgnoreCase))
            {
                Errors[nameof(Program.Code)] = new[] {DomainValidationErrorMessages.Duplicated("رمز البرنامج")};
            }
        }

        return Errors;
    }
}