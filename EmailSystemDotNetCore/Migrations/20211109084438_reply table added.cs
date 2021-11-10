using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmailSystemDotNetCore.Migrations
{
    public partial class replytableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReplyMails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SenderUserModelId = table.Column<string>(nullable: true),
                    ReceiverUserModelId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    MarkAsRead = table.Column<bool>(nullable: false),
                    mailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyMails_AspNetUsers_ReceiverUserModelId",
                        column: x => x.ReceiverUserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyMails_AspNetUsers_SenderUserModelId",
                        column: x => x.SenderUserModelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyMails_Mails_mailId",
                        column: x => x.mailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyMails_ReceiverUserModelId",
                table: "ReplyMails",
                column: "ReceiverUserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyMails_SenderUserModelId",
                table: "ReplyMails",
                column: "SenderUserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyMails_mailId",
                table: "ReplyMails",
                column: "mailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyMails");
        }
    }
}
