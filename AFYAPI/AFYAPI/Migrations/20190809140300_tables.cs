using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AFYAPI.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AFYEmployee",
                columns: table => new
                {
                    AFYEmployeeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AFYEmployee", x => x.AFYEmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "AFYHoliday",
                columns: table => new
                {
                    AFYHolidayId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Holiday = table.Column<DateTime>(nullable: false),
                    HolidayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AFYHoliday", x => x.AFYHolidayId);
                });

            migrationBuilder.CreateTable(
                name: "AFYWorkingAccount",
                columns: table => new
                {
                    AFYWorkingAccountId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmployeeAFYEmployeeId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ShouldWorkingHours = table.Column<decimal>(nullable: false),
                    ShouldWorkingHoursFull = table.Column<decimal>(nullable: false),
                    WorkingHourPercentage = table.Column<decimal>(nullable: false),
                    HolydaysCount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AFYWorkingAccount", x => x.AFYWorkingAccountId);
                    table.ForeignKey(
                        name: "FK_AFYWorkingAccount_AFYEmployee_EmployeeAFYEmployeeId",
                        column: x => x.EmployeeAFYEmployeeId,
                        principalTable: "AFYEmployee",
                        principalColumn: "AFYEmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AFYWorkingTime",
                columns: table => new
                {
                    AFYWorkingTimeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Week = table.Column<int>(nullable: false),
                    WorkingDay = table.Column<DateTime>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    HoursWorked = table.Column<decimal>(nullable: false),
                    HoursToWork = table.Column<decimal>(nullable: false),
                    Difference = table.Column<decimal>(nullable: false),
                    AccountAFYWorkingAccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AFYWorkingTime", x => x.AFYWorkingTimeId);
                    table.ForeignKey(
                        name: "FK_AFYWorkingTime_AFYWorkingAccount_AccountAFYWorkingAccountId",
                        column: x => x.AccountAFYWorkingAccountId,
                        principalTable: "AFYWorkingAccount",
                        principalColumn: "AFYWorkingAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AFYWorkingAccount_EmployeeAFYEmployeeId",
                table: "AFYWorkingAccount",
                column: "EmployeeAFYEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AFYWorkingTime_AccountAFYWorkingAccountId",
                table: "AFYWorkingTime",
                column: "AccountAFYWorkingAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AFYHoliday");

            migrationBuilder.DropTable(
                name: "AFYWorkingTime");

            migrationBuilder.DropTable(
                name: "AFYWorkingAccount");

            migrationBuilder.DropTable(
                name: "AFYEmployee");
        }
    }
}
