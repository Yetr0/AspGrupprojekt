using Microsoft.EntityFrameworkCore.Migrations;

namespace Event.Migrations
{
    public partial class AddedAttendeesToEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_MyUserId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "MyUserId",
                table: "Events",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_MyUserId",
                table: "Events",
                newName: "IX_Events_OrganizerId");

            migrationBuilder.CreateTable(
                name: "EventsMyUser",
                columns: table => new
                {
                    AttendeesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MyEventsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsMyUser", x => new { x.AttendeesId, x.MyEventsId });
                    table.ForeignKey(
                        name: "FK_EventsMyUser_AspNetUsers_AttendeesId",
                        column: x => x.AttendeesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsMyUser_Events_MyEventsId",
                        column: x => x.MyEventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsMyUser_MyEventsId",
                table: "EventsMyUser",
                column: "MyEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventsMyUser");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Events",
                newName: "MyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                newName: "IX_Events_MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_MyUserId",
                table: "Events",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
