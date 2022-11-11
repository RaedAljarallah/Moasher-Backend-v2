﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moasher.Domain.Entities;
using Moasher.Domain.Enums;

namespace Moasher.Persistence;

public class MoasherDbContextInitializer
{
    private readonly ILogger<MoasherDbContextInitializer> _logger;
    private readonly MoasherDbContext _context;

    public MoasherDbContextInitializer(ILogger<MoasherDbContextInitializer> logger, MoasherDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync(cancellationToken: cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database");
            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        try
        {
            await TrySeedAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    public async Task TrySeedAsync(CancellationToken cancellationToken)
    {
        var hasDefaultInitiativeStatus = await _context.EnumTypes
            .AnyAsync(e => e.Category.ToString() == EnumTypeCategory.InitiativeStatus.ToString() && e.IsDefault, cancellationToken: cancellationToken);
        if (!hasDefaultInitiativeStatus)
        {
            _context.EnumTypes.Add(new EnumType
            {
                Category = EnumTypeCategory.InitiativeStatus.ToString(),
                Name = "لا توجد جالة",
                Style = "gray-1",
                CanBeDeleted = false,
                IsDefault = true,
                CreatedBy = "System",
                CreatedAt = DateTimeOffset.Now.AddHours(3)
            });

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
    }
}