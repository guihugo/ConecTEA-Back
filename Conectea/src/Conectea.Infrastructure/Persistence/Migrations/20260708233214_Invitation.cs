using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Conectea.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Invitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientGuardians_AspNetUsers_GuardianId",
                table: "PatientGuardians");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientTherapists_AspNetUsers_TherapistId",
                table: "PatientTherapists");

            migrationBuilder.CreateTable(
                name: "GuardianInvitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuardianInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuardianInvitations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guardians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guardians_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Therapists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Therapists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuardianInvitations_PatientId",
                table: "GuardianInvitations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Guardians_UserId",
                table: "Guardians",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Therapists_UserId",
                table: "Therapists",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientGuardians_Guardians_GuardianId",
                table: "PatientGuardians",
                column: "GuardianId",
                principalTable: "Guardians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTherapists_Therapists_TherapistId",
                table: "PatientTherapists",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientGuardians_Guardians_GuardianId",
                table: "PatientGuardians");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientTherapists_Therapists_TherapistId",
                table: "PatientTherapists");

            migrationBuilder.DropTable(
                name: "GuardianInvitations");

            migrationBuilder.DropTable(
                name: "Guardians");

            migrationBuilder.DropTable(
                name: "Therapists");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientGuardians_AspNetUsers_GuardianId",
                table: "PatientGuardians",
                column: "GuardianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTherapists_AspNetUsers_TherapistId",
                table: "PatientTherapists",
                column: "TherapistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
