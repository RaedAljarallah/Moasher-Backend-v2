using System.Collections.ObjectModel;

namespace Moasher.Application.Common.Constants;

public struct Actions
{
    public const string Create = nameof(Create);
    public const string View = nameof(View);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);

    public static IReadOnlyList<string> All = new ReadOnlyCollection<string>(new[]
    {
        Create,
        View,
        Update,
        Delete,
        Export
    });
}