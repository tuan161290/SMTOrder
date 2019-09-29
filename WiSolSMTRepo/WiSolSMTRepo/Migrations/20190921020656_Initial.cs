using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WiSolSMTRepo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineInfos",
                columns: table => new
                {
                    LineInfoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineInfos", x => x.LineInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanInfoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Order = table.Column<int>(nullable: false),
                    Elapsed = table.Column<int>(nullable: false),
                    Remain = table.Column<int>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    FinishedTime = table.Column<DateTime>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    LineInfoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanInfoID);
                    table.ForeignKey(
                        name: "FK_Plans_LineInfos_LineInfoID",
                        column: x => x.LineInfoID,
                        principalTable: "LineInfos",
                        principalColumn: "LineInfoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plans_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ConfirmedTime = table.Column<DateTime>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    LineInfoID = table.Column<int>(nullable: false),
                    StopDuration = table.Column<int>(nullable: false),
                    PlanInfoID = table.Column<int>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    Reason = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_LineInfos_LineInfoID",
                        column: x => x.LineInfoID,
                        principalTable: "LineInfos",
                        principalColumn: "LineInfoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Plans_PlanInfoID",
                        column: x => x.PlanInfoID,
                        principalTable: "Plans",
                        principalColumn: "PlanInfoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 1, false, "SMT-A" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 2, false, "SMT-B" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 3, false, "SMT-C" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 4, false, "SMT-D" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 5, false, "SMT-E" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 6, false, "SMT-F" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 7, false, "SMT-G" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 8, false, "SMT-H" });

            migrationBuilder.InsertData(
                table: "LineInfos",
                columns: new[] { "LineInfoID", "Is_active", "Name" },
                values: new object[] { 9, false, "SMT-I" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Name" },
                values: new object[] { 1, "L7E0" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Name" },
                values: new object[] { 2, "L7E1" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LineInfoID",
                table: "Orders",
                column: "LineInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PlanInfoID",
                table: "Orders",
                column: "PlanInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductID",
                table: "Orders",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_LineInfoID",
                table: "Plans",
                column: "LineInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_ProductID",
                table: "Plans",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "LineInfos");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
