using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMangement.Infrastructure.EFCore.Migrations
{
    public partial class EditTable_InventoryOperartion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationId",
                table: "inventoryOperations",
                newName: "OperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperatorId",
                table: "inventoryOperations",
                newName: "OperationId");
        }
    }
}
