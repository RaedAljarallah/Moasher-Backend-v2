using AutoMapper;
using Moasher.Application.Features.EditRequests;
using Moasher.Domain.Entities.EditRequests;

namespace Moasher.Application.Common.Mappings;

public class EditRequestMappings : Profile
{
    public EditRequestMappings()
    {
        CreateMap<EditRequest, EditRequestDto>()
            .ForMember(e => e.Scopes, opt => opt.MapFrom(e => e.GetEditScopes()));
    }
}