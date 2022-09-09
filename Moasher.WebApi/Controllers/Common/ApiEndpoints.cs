namespace Moasher.WebApi.Controllers.Common;

public struct ApiEndpoints
{
    public struct Initiatives
    {
        private const string Base = "initiatives";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
}