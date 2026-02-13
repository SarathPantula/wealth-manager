namespace wealth_manager.Api.Models;

/// <summary>
/// Entity representing a gold asset with value and purity (karat).
/// </summary>
public class GoldAsset
{
    /// <summary>Unique identifier for the gold asset.</summary>
    public Guid Id { get; set; }

    /// <summary>Monetary or weight value of the gold asset.</summary>
    public decimal Value { get; set; }

    /// <summary>Purity of the gold in karats (BIS standard).</summary>
    public GoldKarat Karat { get; set; }

    /// <summary>UTC timestamp when the record was created. Set once and never updated.</summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>UTC timestamp when the record was last modified.</summary>
    public DateTime ModifiedAt { get; init; }
}
