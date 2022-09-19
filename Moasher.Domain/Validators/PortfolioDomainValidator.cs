using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Common.Constants;
using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Entities;

namespace Moasher.Domain.Validators;

public class PortfolioDomainValidator : DomainValidator, IDomainValidator
{
    private readonly List<Portfolio> _portfolios;
    private readonly string _name;
    private readonly string _code;

    public PortfolioDomainValidator(List<Portfolio> portfolios, string name, string code)
    {
        _portfolios = portfolios;
        _name = name;
        _code = code;
    }
    
    public IDictionary<string, string[]> Validate()
    {
        foreach (var portfolio in _portfolios)
        {
            if (portfolio.Name == _name)
            {
                Errors["Name"] = new[] {DomainValidationErrorMessages.Duplicated("اسم المحفظة")};
            }

            if (portfolio.Code == _code)
            {
                Errors["Code"] = new[] {DomainValidationErrorMessages.Duplicated("رمز المحفظة")};
            }
        }
        
        return Errors;
    }
}