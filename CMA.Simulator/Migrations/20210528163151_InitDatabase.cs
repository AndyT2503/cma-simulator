using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMA_Simulator.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    ContainerNumber = table.Column<string>(type: "text", nullable: false),
                    LeaseType = table.Column<string>(type: "text", nullable: true),
                    BuildYearAndCountry = table.Column<string>(type: "text", nullable: true),
                    EquipmentSizeType = table.Column<string>(type: "text", nullable: true),
                    LastEvent = table.Column<string>(type: "text", nullable: true),
                    LasEventDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreditTermsRule = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.ContainerNumber);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<string>(type: "text", nullable: false),
                    CargoShipmetId = table.Column<string>(type: "text", nullable: true),
                    ClosingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ContainerHandlingReference = table.Column<string>(type: "text", nullable: true),
                    Destination = table.Column<string>(type: "text", nullable: true),
                    SalingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ShipperOwnedBooking = table.Column<bool>(type: "boolean", nullable: false),
                    PolWaypoint = table.Column<string>(type: "text", nullable: true),
                    Voyage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                });

            migrationBuilder.CreateTable(
                name: "CargoEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedContainerNumber = table.Column<string>(type: "text", nullable: true),
                    EquipmentSizeType = table.Column<string>(type: "text", nullable: true),
                    Grade = table.Column<string>(type: "text", nullable: true),
                    Commodity = table.Column<string>(type: "text", nullable: true),
                    ShipmentId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoEquipments_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoEquipments_ShipmentId",
                table: "CargoEquipments",
                column: "ShipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoEquipments");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Shipments");
        }
    }
}
