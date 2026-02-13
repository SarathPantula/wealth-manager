using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wealth_manager.Api.Data.Migrations;

[Migration("20260213160000_InitialCreate")]
/// <inheritdoc />
/// <summary>
/// Initial Code First schema. Must stay in sync with WealthManagerDbContext and WealthManagerDbContextModelSnapshot.
/// GoldAsset: id (uuid, gen_random_uuid()), Value (numeric(18,2)), Karat (int), createdAt (timestamptz), modifiedAt (timestamptz, now()).
/// </summary>
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "GoldAsset",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                Value = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                Karat = table.Column<int>(type: "integer", nullable: false), // GoldKarat enum stored as int (BIS karats: 10,12,14,18,20,22,24)
                createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                modifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GoldAsset", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "GoldAsset");
    }
}
