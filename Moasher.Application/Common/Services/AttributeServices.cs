using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Moasher.Application.Common.Services;

public static class AttributeServices
{
    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic;

    public static string GetDisplayName<TAssemblySource>(string className)
    {
        try
        {
            var classType = GetType<TAssemblySource>(className);
            return classType?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() is DisplayAttribute displayAttribute
                ? displayAttribute.Name ?? className
                : className;
        }
        catch (Exception)
        {
            return className;
        }
    }

    public static string GetDisplayName<TAssemblySource>(string className, string propertyName)
    {
        try
        {
            object[]? attributes;
            var type = GetType<TAssemblySource>(className);
            var field = type?.GetField(propertyName, Flags);
            if (field is not null)
            {
                attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false);
            }
            else
            {
                attributes = type?.GetProperties()
                    .FirstOrDefault(x => x.Name == propertyName)?
                    .GetCustomAttributes(typeof(DisplayAttribute), false);
            }

            return attributes?
                .FirstOrDefault() is DisplayAttribute displayAttribute
                ? displayAttribute.Name ?? propertyName
                : propertyName;
        }
        catch (Exception)
        {
            return propertyName;
        }
    }

    public static IEnumerable<string> GetFieldNames<TAssemblySource>(string className)
    {
        try
        {
            var type = GetType<TAssemblySource>(className);
            return type?.GetProperties().Select(p => p.Name) ?? Enumerable.Empty<string>();
        }
        catch (Exception)
        {
            return Enumerable.Empty<string>();
        }
    }

    public static Type? GetType<TAssemblySource>(string className)
    {
        return Assembly.GetAssembly(typeof(TAssemblySource))?
            .GetTypes()
            .FirstOrDefault(x => x.Name == className);
    }
    
    public static TType? CreateInstance<TType>(Type type, params object[] args)
    {
        return (TType?)Activator.CreateInstance(type, args);
    }
}