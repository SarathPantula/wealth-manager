using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wealth_manager.Api.Data;
using wealth_manager.Api.Models;

namespace wealth_manager.Api.Controllers;

/// <summary>
/// API controller for CRUD operations on gold assets.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GoldAssetsController : ControllerBase
{
    private readonly WealthManagerDbContext _db;

    /// <summary>Creates a new instance of the controller with the given database context.</summary>
    /// <param name="db">The wealth manager database context.</param>
    public GoldAssetsController(WealthManagerDbContext db)
    {
        _db = db;
    }

    /// <summary>Gets all gold assets, ordered by creation date (newest first).</summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of gold asset responses.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GoldAssetResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GoldAssetResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var list = await _db.GoldAssets
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => new GoldAssetResponse(e.Id, e.Grams, e.Karat, e.CreatedAt, e.ModifiedAt))
            .ToListAsync(cancellationToken);
        return Ok(list);
    }

    /// <summary>Gets a single gold asset by its unique identifier.</summary>
    /// <param name="id">The gold asset identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The gold asset if found; otherwise 404 Not Found.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GoldAssetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoldAssetResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.GoldAssets.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();
        return Ok(ToResponse(entity));
    }

    /// <summary>Creates a new gold asset.</summary>
    /// <param name="request">The create request (grams and karat).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created gold asset (201) with Location header; or 400 if invalid.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GoldAssetResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GoldAssetResponse>> Create(
        [FromBody] CreateGoldAssetRequest request,
        CancellationToken cancellationToken)
    {
        var entity = new GoldAsset
        {
            Grams = request.Grams,
            Karat = request.Karat
        };
        _db.GoldAssets.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, ToResponse(entity));
    }

    /// <summary>Updates an existing gold asset by id.</summary>
    /// <param name="id">The gold asset identifier.</param>
    /// <param name="request">The update request (grams and karat).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated gold asset if found; otherwise 404 Not Found.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(GoldAssetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoldAssetResponse>> Update(
        Guid id,
        [FromBody] UpdateGoldAssetRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _db.GoldAssets.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();
        entity.Grams = request.Grams;
        entity.Karat = request.Karat;
        await _db.SaveChangesAsync(cancellationToken);
        return Ok(ToResponse(entity));
    }

    /// <summary>Deletes a gold asset by id.</summary>
    /// <param name="id">The gold asset identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>204 No Content if deleted; otherwise 404 Not Found.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.GoldAssets.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();
        _db.GoldAssets.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    private static GoldAssetResponse ToResponse(GoldAsset e) =>
        new(e.Id, e.Grams, e.Karat, e.CreatedAt, e.ModifiedAt);
}
