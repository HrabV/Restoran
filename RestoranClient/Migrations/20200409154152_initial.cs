using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoranClient.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abonent",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abonent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClientCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    discount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waiters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waiters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    waiter_id = table.Column<int>(nullable: true),
                    abonent_id = table.Column<int>(nullable: true),
                    time_order = table.Column<DateTime>(nullable: false),
                    Bill = table.Column<decimal>(nullable: true),
                    source_id = table.Column<int>(nullable: true),
                    FixedSource = table.Column<string>(nullable: false),
                    end_order = table.Column<DateTime>(nullable: true),
                    Paid = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_abonent_abonent_id",
                        column: x => x.abonent_id,
                        principalTable: "abonent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    items_id = table.Column<int>(nullable: false),
                    order_id = table.Column<int>(nullable: false),
                    bill = table.Column<decimal>(nullable: false),
                    count = table.Column<decimal>(nullable: false),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Order_order_id",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Details",
                columns: new[] { "Id", "bill", "count", "items_id", "order_id", "price" },
                values: new object[,]
                {
                    { 1, 10.000m, 2.000m, 2, 1, 50.00m },
                    { 7, 1125.00m, 25.000m, 4, 10, 45.00m },
                    { 6, 75.00m, 3.000m, 7, 5, 25.00m },
                    { 5, 50.00m, 2.000m, 7, 5, 25.00m },
                    { 4, 45.00m, 1.000m, 4, 4, 45.00m },
                    { 3, 150.00m, 0.500m, 14, 3, 300.00m },
                    { 2, 90.00m, 2.000m, 8, 2, 45.00m },
                    { 8, 1125.00m, 25.000m, 4, 11, 45.00m },
                    { 9, 75.00m, 3.000m, 7, 11, 25.00m }
                });

            migrationBuilder.InsertData(
                table: "Waiters",
                columns: new[] { "id", "name", "password" },
                values: new object[,]
                {
                    { 1, "Маріка", "11112222" },
                    { 2, "Катерина", "22222222" },
                    { 3, "Жужа", "33332222" },
                    { 4, "Лєнка", "44442222" },
                    { 5, "Валєра", "55552222" },
                    { 6, "Валентина", "66662222" },
                    { 7, "Олександр", "77772222" },
                    { 8, "Степан", "88882222" }
                });

            migrationBuilder.InsertData(
                table: "abonent",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "table 1" },
                    { 2, "table 2" },
                    { 3, "table 3" },
                    { 4, "table 4" },
                    { 8, "table 8" },
                    { 5, "table 5" },
                    { 6, "table 6" },
                    { 7, "table 7" }
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "id", "name", "price" },
                values: new object[,]
                {
                    { 2, "Borch", 50.00m },
                    { 16, "Whiskey", 1000.00m },
                    { 15, "Water", 40.00m },
                    { 14, "Vodka", 300.00m },
                    { 21, "Teckila", 56.00m },
                    { 13, "Shashlik", 300.00m },
                    { 12, "Salad", 100.00m },
                    { 17, "Russian salat", 34.00m },
                    { 11, "Pizza", 95.00m },
                    { 10, "Ketchup", 300.00m },
                    { 9, "IceCream", 50.00m },
                    { 20, "White coffe", 56.00m },
                    { 18, "Marenga", 243.00m },
                    { 8, "Duck soup", 45.00m },
                    { 7, "Cofee", 25.00m },
                    { 5, "chicken with poatoes", 95.00m },
                    { 4, "Chicken soup", 45.00m },
                    { 3, "bread", 5.00m },
                    { 1, "Bear", 45.00m },
                    { 6, "Coca Cola", 40.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_order_id",
                table: "Details",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_abonent_id",
                table: "Order",
                column: "abonent_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCards");

            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "Waiters");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "abonent");
        }
    }
}
