using Microsoft.AspNetCore.Identity;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Extensions;

public static class UserManagerExtensions
{
    public static string GeneratePassword(this UserManager<User> userManager)
    {
        var options = userManager.Options.Password;
        var randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!$?_-"                        // non-alphanumeric
        };
        
        Random rand = new(Environment.TickCount);
        List<char> chars = new();

        if (options.RequireUppercase)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[0][rand.Next(0, randomChars[0].Length)]);

        if (options.RequireLowercase)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[1][rand.Next(0, randomChars[1].Length)]);

        if (options.RequireDigit)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[2][rand.Next(0, randomChars[2].Length)]);

        if (options.RequireNonAlphanumeric)
            chars.Insert(rand.Next(0, chars.Count),
                randomChars[3][rand.Next(0, randomChars[3].Length)]);

        for (var i = chars.Count; i < options.RequiredLength
                                  || chars.Distinct().Count() < options.RequiredUniqueChars; i++)
        {
            var rcs = randomChars[rand.Next(0, randomChars.Length)];
            chars.Insert(rand.Next(0, chars.Count),
                rcs[rand.Next(0, rcs.Length)]);
        }

        return $"Moasher@{new string(chars.ToArray())}";
    }
}