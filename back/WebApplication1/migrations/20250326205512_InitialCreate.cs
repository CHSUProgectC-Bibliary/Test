using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Book_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Section = table.Column<string>(type: "character varying", nullable: false),
                    Title = table.Column<string>(type: "character varying", nullable: false),
                    Author = table.Column<string>(type: "character varying", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Book_Id);
                });
            migrationBuilder.CreateTable(
               name: "Users",
               columns: table => new
               {
                   User_Id = table.Column<int>(type: "integer", nullable: false)
                       .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                   Username = table.Column<string>(type: "character varying", nullable: false),
                   Email = table.Column<string>(type: "character varying", nullable: false),
                   Password = table.Column<string>(type: "character varying", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Users", x => x.User_Id);
               });
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Review_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Id = table.Column<int>(type: "integer", nullable: false),
                    Book_Id = table.Column<int>(type: "integer", nullable: false),
                    Book_Title_Auth=table.Column<string>(type:"character varying",nullable:false),
                    Review_Date=table.Column<DateTime>(type:"date",nullable:false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Review_Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Books",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name:"FK_Reviews_Users_User_Id",
                        column:x=>x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
             migrationBuilder.CreateIndex(
                name: "IX_Reviews_Book_Id",
                table: "Reviews",
                column: "Book_Id");
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_User_Id",
                table: "Reviews",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
