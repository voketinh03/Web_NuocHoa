using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CK_ASP_NET_CORE.Migrations
{
    public partial class TaoDatHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenNguoiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdSanPham = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tenNguoiDat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderModels");
        }
    }
}
