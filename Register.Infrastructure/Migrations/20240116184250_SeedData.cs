using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Register.Infrastructure.Migrations {
    /// <inheritdoc />
    public partial class SeedData : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("INSERT INTO [Roles] ([Name]) values('Admin');\r\nINSERT INTO [Roles] ([Name]) values('User');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {

        }
    }
}
