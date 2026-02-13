namespace wealth_manager.Api.Models;

/// <summary>
/// BIS (Bureau of Indian Standards) recognised gold purity in karats.
/// Stored as int in the database.
/// </summary>
public enum GoldKarat
{
    /// <summary>10 karat gold (41.7% purity).</summary>
    Karat10 = 10,

    /// <summary>12 karat gold (50% purity).</summary>
    Karat12 = 12,

    /// <summary>14 karat gold (58.3% purity).</summary>
    Karat14 = 14,

    /// <summary>18 karat gold (75% purity).</summary>
    Karat18 = 18,

    /// <summary>20 karat gold (83.3% purity).</summary>
    Karat20 = 20,

    /// <summary>22 karat gold (91.7% purity).</summary>
    Karat22 = 22,

    /// <summary>24 karat gold (99.9% purity, fine gold).</summary>
    Karat24 = 24
}
