namespace Moasher.Application.Common.Types;

public record Permission(string Action, string Resource)
{
    public override string ToString()
    {
        return $"{nameof(Permission)}.{Action}.{Resource}";
    }

    public static Permission InitializeFromPolicy(string policyName)
    {
        if (policyName.StartsWith(nameof(Permission), StringComparison.CurrentCultureIgnoreCase))
        {
            policyName = policyName.Replace($"{nameof(Permission)}.", string.Empty, StringComparison.CurrentCultureIgnoreCase);
        }

        var values = policyName.Split(".");
        return new Permission(values[0], values[1]);
    }
}