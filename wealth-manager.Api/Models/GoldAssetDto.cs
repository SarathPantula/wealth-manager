namespace wealth_manager.Api.Models;

/// <summary>
/// Request body for creating a new gold asset.
/// </summary>
/// <param name="Value">Monetary or weight value of the gold asset.</param>
/// <param name="Karat">Purity of the gold in karats (BIS standard).</param>
public record CreateGoldAssetRequest(decimal Value, GoldKarat Karat);

/// <summary>
/// Request body for updating an existing gold asset.
/// </summary>
/// <param name="Value">Monetary or weight value of the gold asset.</param>
/// <param name="Karat">Purity of the gold in karats (BIS standard).</param>
public record UpdateGoldAssetRequest(decimal Value, GoldKarat Karat);

/// <summary>
/// Response model for a gold asset (returned by API endpoints).
/// </summary>
/// <param name="Id">Unique identifier for the gold asset.</param>
/// <param name="Value">Monetary or weight value of the gold asset.</param>
/// <param name="Karat">Purity of the gold in karats (BIS standard).</param>
/// <param name="CreatedAt">UTC timestamp when the record was created.</param>
/// <param name="ModifiedAt">UTC timestamp when the record was last modified.</param>
public record GoldAssetResponse(Guid Id, decimal Value, GoldKarat Karat, DateTime CreatedAt, DateTime ModifiedAt);
