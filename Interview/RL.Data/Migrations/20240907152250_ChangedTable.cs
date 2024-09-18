using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class ChangedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AssignedUsers_ProcedureId",
                table: "AssignedUsers",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedUsers_Plans_PlanId",
                table: "AssignedUsers",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedUsers_Procedures_ProcedureId",
                table: "AssignedUsers",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "ProcedureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedUsers_Plans_PlanId",
                table: "AssignedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignedUsers_Procedures_ProcedureId",
                table: "AssignedUsers");

            migrationBuilder.DropIndex(
                name: "IX_AssignedUsers_ProcedureId",
                table: "AssignedUsers");
        }
    }
}
