using FluentValidation.Results;

namespace Moasher.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(string failure)
        : this()
    {
        Errors = new Dictionary<string, string[]>
        {
            {"", new[] {failure}}
        };
    }

    public ValidationException(IDictionary<string, string[]> failures)
        : this()
    {
        Errors = failures.ToDictionary(e => e.Key, e => e.Value);
    }

    public ValidationException(string key, string failure)
        : this()
    {
        Errors = new Dictionary<string, string[]>
        {
            {key, new[] {failure}}
        };
    }
}