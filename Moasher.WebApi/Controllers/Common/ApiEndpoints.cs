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
        public const string Edit = $"{Base}/{{id}}/edit";
    }
    
    public struct KPIs
    {
        private const string Base = "kpis";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
    }
    
    public struct Portfolios
    {
        private const string Base = "portfolios";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
    }
    
    public struct Entities
    {
        private const string Base = "entities";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
    
    public struct Programs
    {
        private const string Base = "programs";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
    
    public struct StrategicObjectives
    {
        private const string Base = "strategic-objectives";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
    
    public struct EnumTypes
    {
        private const string Base = "enum-types";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
    
    public struct Milestones
    {
        private const string Base = "milestones";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
}