using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Moasher.Application.Common.Services;

public static class HierarchyIdService
{
    public static bool IsValidHierarchyId(string input)
    {
        input = ConvertToHierarchyIdFormat(input);
        const string pattern = @"^(\/-?(?!0)\d+(\.(-?(?!0)\d+))*)*\/$";
        var re = new Regex(pattern);
        return re.IsMatch(input);
    }
    
    public static HierarchyId Parse(string input)
    {
        input = ConvertToHierarchyIdFormat(input);
        return HierarchyId.Parse(input);
    }
    
    private static string ConvertToHierarchyIdFormat(string input)
    {
        if (!input.StartsWith("/"))
        {
            input = $"/{input}";
        }

        if (!input.EndsWith("/"))
        {
            input = $"{input}/";
        }

        if (input.Contains("."))
        {
            input = input.Replace(".", "/");
        }

        return input;
    }
}