using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Booking_New.Migrations
{
    /// <inheritdoc />
    public partial class HotelsBookingchagedTabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Feedbacks");
        }
    }
}
