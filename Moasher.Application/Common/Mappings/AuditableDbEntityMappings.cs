using AutoMapper;
using Moasher.Application.Common.Abstracts;
using Moasher.Domain.Common.Abstracts;

namespace Moasher.Application.Common.Mappings;

public class AuditableDbEntityMappings : Profile
{
    public AuditableDbEntityMappings()
    {
        CreateMap<AuditableDbEntity<Guid>, DtoBase>()
            .ForMember(e => e.Audit, opt => opt.MapFrom(e => new AuditDto
            {
                CreatedBy = e.CreatedBy,
                CreatedAt = e.CreatedAt,
                LastModifiedBy = e.LastModifiedBy,
                LastModified = e.LastModified
            }));
    }
}