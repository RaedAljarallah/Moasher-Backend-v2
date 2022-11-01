using AutoMapper;
using Moasher.Application.Features.SearchRecords;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Mappings;

public class SearchRecordMappings : Profile
{
    public SearchRecordMappings()
    {
        CreateMap<Search, SearchRecordDto>();
    }
}