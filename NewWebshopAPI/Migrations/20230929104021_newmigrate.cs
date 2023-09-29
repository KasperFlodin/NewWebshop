﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NewWebshopAPI.Migrations
{
    /// <inheritdoc />
    public partial class newmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    Photolink = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(4)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "Photolink", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "S200", "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg", 3000, "Bed" },
                    { 2, "WoodTable", "https://livin.dk/wp-content/uploads/2020/07/spisebord-lakeret-eg-1.jpg", 1200, "Table" },
                    { 3, "WoodChair", "https://cdn.shopify.com/s/files/1/0810/1821/products/Emma-bla-velour-stol-11706.jpg", 299, "Chair" },
                    { 4, "PlasticChair", "https://cdn.shopify.com/s/files/1/0810/1821/products/Emma-bla-velour-stol-11706.jpg", 99, "Chair" },
                    { 5, "Sleepr", "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg", 5000, "Bed" },
                    { 6, "z00", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_1lHnZy9bZidanyjBJr5JiIuSxNX1Y2LM0_HpV1TQrdwMLbUHY1kcfC12pxme6jzb9qw&usqp=CAU", 600, "Fence" },
                    { 7, "Beding", "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg", 6000, "Bed" },
                    { 8, "alsoBed", "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg", 7000, "Bed" },
                    { 9, "Keeper", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_1lHnZy9bZidanyjBJr5JiIuSxNX1Y2LM0_HpV1TQrdwMLbUHY1kcfC12pxme6jzb9qw&usqp=CAU", 1000, "Fence" },
                    { 10, "zzz", "https://aca8cd9d105dbd447097-f6f51e4cef559c9308eef9d726fd38a7.ssl.cf1.rackcdn.com/600262-2.jpg", 8000, "Bed" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "City", "Email", "FirstName", "LastName", "Password", "Phone", "Role", "Zip" },
                values: new object[,]
                {
                    { 1, "Meterskoven 1", "Byen", "Peter.lund@gmail.com", "Peter", "Lund", "123456", "12345678", 0, "4321" },
                    { 2, "Skoven 2", "Tarn", "Simon.green@gmail.com", "Simon", "Green", "123456", "11223344", 1, "1144" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
