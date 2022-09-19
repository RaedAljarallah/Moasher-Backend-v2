using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Application.Features.Portfolios;
using Moasher.Application.Features.Portfolios.Commands.CreatePortfolio;
using Moasher.Application.Features.Portfolios.Commands.UpdatePortfolio;
using Moasher.Application.Features.Portfolios.Queries.EditPortfolio;
using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class PortfolioMappings : Profile
{
    public PortfolioMappings()
    {
        CreateMap<Portfolio, PortfolioDto>()
            .IncludeBase<AuditableDbEntity, DtoBase>();
        
        CreateMap<Portfolio, EditPortfolioDto>()
            .ForMember(p => p.RelatedInitiatives, opt => opt.MapFrom(p => p.Initiatives));
        
        CreateMap<CreatePortfolioCommand, Portfolio>();
        
        CreateMap<UpdatePortfolioCommand, Portfolio>()
            .ForMember(p => p.Id, opt => opt.Ignore());
    }
}