using Moasher.Application.Features.Entities;
using Moasher.Domain.Entities;

namespace Moasher.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    public byte[] BuildEntitiesFile(IEnumerable<EntityDto> entities);
}