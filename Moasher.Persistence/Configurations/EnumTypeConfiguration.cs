using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moasher.Domain.Entities;
using Moasher.Persistence.Configurations.Abstracts;

namespace Moasher.Persistence.Configurations;

public class EnumTypeConfiguration : ConfigurationBase<EnumType>
{
}