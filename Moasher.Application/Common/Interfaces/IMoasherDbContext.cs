using Microsoft.EntityFrameworkCore;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Application.Common.Interfaces;

public interface IMoasherDbContext
{
    public DbSet<Initiative> Initiatives { get; }
}