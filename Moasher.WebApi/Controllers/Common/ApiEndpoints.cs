namespace Moasher.WebApi.Controllers.Common;

public struct ApiEndpoints
{
    public struct Initiatives
    {
        private const string Base = "initiatives";
        public const string All = Base;
        public const string Summary = $"{Base}/summary";
        public const string Progress = $"{Base}/progress";
        public const string StatusProgress = $"{Base}/status-progress";
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }
    
    public struct KPIs
    {
        private const string Base = "kpis";
        public const string All = Base;
        public const string Progress = $"{Base}/progress";
        public const string StatusProgress = $"{Base}/status-progress";
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }
    
    public struct KPIValues
    {
        private const string Base = "kpi-values";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
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
        public const string Export = $"{Base}/export";
    }
    
    public struct Programs
    {
        private const string Base = "programs";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
    }
    
    public struct StrategicObjectives
    {
        private const string Base = "strategic-objectives";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
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
        public const string Export = $"{Base}/export";
    }
    
    public struct Deliverables
    {
        private const string Base = "deliverables";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
    }
    
    public struct ApprovedCosts
    {
        private const string Base = "approved-costs";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
    }
    
    public struct Budgets
    {
        private const string Base = "budgets";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
    }
    
    public struct Issues
    {
        private const string Base = "issues";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }
    
    public struct Risks
    {
        private const string Base = "risks";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }
    
    public struct Projects
    {
        private const string Base = "projects";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }
    
    public struct Contracts
    {
        private const string Base = "contracts";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Summary = $"{Base}/summary";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }

    public struct Expenditures
    {
        private const string Base = "expenditures";
        public const string All = Base;
        public const string Summary = $"{Base}/summary";
        public const string Export = $"{Base}/export";
    }
    
    public struct InitiativeTeams
    {
        private const string Base = "initiative-teams";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string Export = $"{Base}/export";
    }
    
    public struct Analytics
    {
        private const string Base = "analytics";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Export = $"{Base}/export";
    }
    
    public struct Users
    {
        private const string Base = "users";
        public const string All = Base;
        public const string Create = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Edit = $"{Base}/{{id}}/edit";
        public const string UpdateSuspensionStatus = $"{Base}/{{id}}/update-suspension-status";
        public const string ResetPassword = $"{Base}/{{id}}/reset-password";
        public const string VerifyActivationToken = $"{Base}/verify-activation-token";
    }

    public struct Notifications
    {
        private const string Base = "notifications";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
        public const string Read = $"{Base}/{{id}}";
    }
    
    public struct Roles
    {
        private const string Base = "roles";
        public const string All = Base;
    }
    
    public struct Search
    {
        private const string Base = "search";
        public const string All = Base;
    }
    
    public struct InvalidTokens
    {
        private const string Base = "invalid-tokens";
        public const string Create = Base;
    }
    
    public struct EditRequests
    {
        private const string Base = "edit-requests";
        public const string All = Base;
        public const string Details = $"{Base}/{{id}}";
        public const string Accept = $"{Base}/accept";
        public const string Reject = $"{Base}/reject";
        public const string Delete = $"{Base}/{{id}}";
    }
}