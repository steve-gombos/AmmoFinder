﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AmmoFinder.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Retailers",
                columns: table => new
                {
                    RetailerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retailers", x => x.RetailerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    Inventory = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoundCount = table.Column<string>(nullable: true),
                    RoundContainer = table.Column<string>(nullable: true),
                    RoundType = table.Column<string>(nullable: true),
                    Caliber = table.Column<string>(nullable: true),
                    Casing = table.Column<string>(nullable: true),
                    Grain = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    RetailerProductId = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    RetailerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Retailers_RetailerId",
                        column: x => x.RetailerId,
                        principalTable: "Retailers",
                        principalColumn: "RetailerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_RetailerId_RetailerProductId",
                table: "Products",
                columns: new[] { "RetailerId", "RetailerProductId" },
                unique: true,
                filter: "[RetailerProductId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Retailers");
        }
    }
}
