using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddGenreToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Genre", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Author 2", "", 11.99m, "Book Title 1" },
                    { 2, "Author 3", "", 12.99m, "Book Title 2" },
                    { 3, "Author 4", "", 13.99m, "Book Title 3" },
                    { 4, "Author 5", "", 14.99m, "Book Title 4" },
                    { 5, "Author 6", "", 10.99m, "Book Title 5" },
                    { 6, "Author 7", "", 11.99m, "Book Title 6" },
                    { 7, "Author 8", "", 12.99m, "Book Title 7" },
                    { 8, "Author 9", "", 13.99m, "Book Title 8" },
                    { 9, "Author 10", "", 14.99m, "Book Title 9" },
                    { 10, "Author 1", "", 10.99m, "Book Title 10" },
                    { 11, "Author 2", "", 11.99m, "Book Title 11" },
                    { 12, "Author 3", "", 12.99m, "Book Title 12" },
                    { 13, "Author 4", "", 13.99m, "Book Title 13" },
                    { 14, "Author 5", "", 14.99m, "Book Title 14" },
                    { 15, "Author 6", "", 10.99m, "Book Title 15" },
                    { 16, "Author 7", "", 11.99m, "Book Title 16" },
                    { 17, "Author 8", "", 12.99m, "Book Title 17" },
                    { 18, "Author 9", "", 13.99m, "Book Title 18" },
                    { 19, "Author 10", "", 14.99m, "Book Title 19" },
                    { 20, "Author 1", "", 10.99m, "Book Title 20" },
                    { 21, "Author 2", "", 11.99m, "Book Title 21" },
                    { 22, "Author 3", "", 12.99m, "Book Title 22" },
                    { 23, "Author 4", "", 13.99m, "Book Title 23" },
                    { 24, "Author 5", "", 14.99m, "Book Title 24" },
                    { 25, "Author 6", "", 10.99m, "Book Title 25" },
                    { 26, "Author 7", "", 11.99m, "Book Title 26" },
                    { 27, "Author 8", "", 12.99m, "Book Title 27" },
                    { 28, "Author 9", "", 13.99m, "Book Title 28" },
                    { 29, "Author 10", "", 14.99m, "Book Title 29" },
                    { 30, "Author 1", "", 10.99m, "Book Title 30" },
                    { 31, "Author 2", "", 11.99m, "Book Title 31" },
                    { 32, "Author 3", "", 12.99m, "Book Title 32" },
                    { 33, "Author 4", "", 13.99m, "Book Title 33" },
                    { 34, "Author 5", "", 14.99m, "Book Title 34" },
                    { 35, "Author 6", "", 10.99m, "Book Title 35" },
                    { 36, "Author 7", "", 11.99m, "Book Title 36" },
                    { 37, "Author 8", "", 12.99m, "Book Title 37" },
                    { 38, "Author 9", "", 13.99m, "Book Title 38" },
                    { 39, "Author 10", "", 14.99m, "Book Title 39" },
                    { 40, "Author 1", "", 10.99m, "Book Title 40" },
                    { 41, "Author 2", "", 11.99m, "Book Title 41" },
                    { 42, "Author 3", "", 12.99m, "Book Title 42" },
                    { 43, "Author 4", "", 13.99m, "Book Title 43" },
                    { 44, "Author 5", "", 14.99m, "Book Title 44" },
                    { 45, "Author 6", "", 10.99m, "Book Title 45" },
                    { 46, "Author 7", "", 11.99m, "Book Title 46" },
                    { 47, "Author 8", "", 12.99m, "Book Title 47" },
                    { 48, "Author 9", "", 13.99m, "Book Title 48" },
                    { 49, "Author 10", "", 14.99m, "Book Title 49" },
                    { 50, "Author 1", "", 10.99m, "Book Title 50" },
                    { 51, "Author 2", "", 11.99m, "Book Title 51" },
                    { 52, "Author 3", "", 12.99m, "Book Title 52" },
                    { 53, "Author 4", "", 13.99m, "Book Title 53" },
                    { 54, "Author 5", "", 14.99m, "Book Title 54" },
                    { 55, "Author 6", "", 10.99m, "Book Title 55" },
                    { 56, "Author 7", "", 11.99m, "Book Title 56" },
                    { 57, "Author 8", "", 12.99m, "Book Title 57" },
                    { 58, "Author 9", "", 13.99m, "Book Title 58" },
                    { 59, "Author 10", "", 14.99m, "Book Title 59" },
                    { 60, "Author 1", "", 10.99m, "Book Title 60" },
                    { 61, "Author 2", "", 11.99m, "Book Title 61" },
                    { 62, "Author 3", "", 12.99m, "Book Title 62" },
                    { 63, "Author 4", "", 13.99m, "Book Title 63" },
                    { 64, "Author 5", "", 14.99m, "Book Title 64" },
                    { 65, "Author 6", "", 10.99m, "Book Title 65" },
                    { 66, "Author 7", "", 11.99m, "Book Title 66" },
                    { 67, "Author 8", "", 12.99m, "Book Title 67" },
                    { 68, "Author 9", "", 13.99m, "Book Title 68" },
                    { 69, "Author 10", "", 14.99m, "Book Title 69" },
                    { 70, "Author 1", "", 10.99m, "Book Title 70" },
                    { 71, "Author 2", "", 11.99m, "Book Title 71" },
                    { 72, "Author 3", "", 12.99m, "Book Title 72" },
                    { 73, "Author 4", "", 13.99m, "Book Title 73" },
                    { 74, "Author 5", "", 14.99m, "Book Title 74" },
                    { 75, "Author 6", "", 10.99m, "Book Title 75" },
                    { 76, "Author 7", "", 11.99m, "Book Title 76" },
                    { 77, "Author 8", "", 12.99m, "Book Title 77" },
                    { 78, "Author 9", "", 13.99m, "Book Title 78" },
                    { 79, "Author 10", "", 14.99m, "Book Title 79" },
                    { 80, "Author 1", "", 10.99m, "Book Title 80" },
                    { 81, "Author 2", "", 11.99m, "Book Title 81" },
                    { 82, "Author 3", "", 12.99m, "Book Title 82" },
                    { 83, "Author 4", "", 13.99m, "Book Title 83" },
                    { 84, "Author 5", "", 14.99m, "Book Title 84" },
                    { 85, "Author 6", "", 10.99m, "Book Title 85" },
                    { 86, "Author 7", "", 11.99m, "Book Title 86" },
                    { 87, "Author 8", "", 12.99m, "Book Title 87" },
                    { 88, "Author 9", "", 13.99m, "Book Title 88" },
                    { 89, "Author 10", "", 14.99m, "Book Title 89" },
                    { 90, "Author 1", "", 10.99m, "Book Title 90" },
                    { 91, "Author 2", "", 11.99m, "Book Title 91" },
                    { 92, "Author 3", "", 12.99m, "Book Title 92" },
                    { 93, "Author 4", "", 13.99m, "Book Title 93" },
                    { 94, "Author 5", "", 14.99m, "Book Title 94" },
                    { 95, "Author 6", "", 10.99m, "Book Title 95" },
                    { 96, "Author 7", "", 11.99m, "Book Title 96" },
                    { 97, "Author 8", "", 12.99m, "Book Title 97" },
                    { 98, "Author 9", "", 13.99m, "Book Title 98" },
                    { 99, "Author 10", "", 14.99m, "Book Title 99" },
                    { 100, "Author 1", "", 10.99m, "Book Title 100" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");
        }
    }
}
