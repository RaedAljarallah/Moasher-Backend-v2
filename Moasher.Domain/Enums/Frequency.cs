using System.ComponentModel.DataAnnotations;

namespace Moasher.Domain.Enums;

public enum Frequency : byte
{
    Annually = 1,
    SemiAnnually = 2,
    Quarterly = 3,
    Monthly = 4
}