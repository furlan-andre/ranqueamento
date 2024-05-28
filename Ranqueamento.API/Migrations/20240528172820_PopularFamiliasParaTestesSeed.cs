using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ranqueamento.API.Migrations
{
    public partial class PopularFamiliasParaTestesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AdicaoDeFamiliasParaTesteSeed.Seed(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
