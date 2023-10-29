using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeX.Core.Migrations
{
    /// <summary>
    /// Originally developed with EF 5.0+
    /// Requirements changed to support older versions of Android (8.0 - API 26).
    /// Manually removed code that is not suppored prior to EF 5.0.  See also
    /// https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations
    /// </summary>
    public partial class ServiceStartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceDateStart",
                table: "UserAccounts",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
