using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "customers",
            columns: table => new
            {
                id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                email = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                identification_number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                phone_number = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                status = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_customers", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "product_categories",
            columns: table => new
            {
                id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_product_categories", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "products",
            columns: table => new
            {
                id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                product_category_id = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                image_uri = table.Column<string>(type: "text", nullable: true),
                status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_products", x => x.id);
                table.ForeignKey(
                    name: "fk_products_product_categories_product_category_id",
                    column: x => x.product_category_id,
                    principalTable: "product_categories",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Customers_Email",
            table: "customers",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Customers_PhoneNumber",
            table: "customers",
            column: "phone_number",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Products_Category_Id",
            table: "products",
            column: "product_category_id");

        migrationBuilder.CreateIndex(
            name: "IX_Products_Name",
            table: "products",
            column: "name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "customers");

        migrationBuilder.DropTable(
            name: "products");

        migrationBuilder.DropTable(
            name: "product_categories");
    }
}
