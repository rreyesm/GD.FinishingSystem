using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.FinishingSystem.DAL.Migrations
{
    public partial class v38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                      CREATE SEQUENCE SeqPackingListFinishing AS INT
                      START WITH 1
                      INCREMENT BY 1
                      GO

                      CREATE SEQUENCE SeqPackingListInspection AS INT
                      START WITH 1
                      INCREMENT BY 1
                      GO
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
