using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class PopulateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Lucas Nunes', 'lucasn@teste.com.br', 'lucas123')");
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Arthur Nunes', 'arthurn@teste.com.br', 'arthur123')");
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Eurias Amorim', 'euriasa@teste.com.br', 'eurias123')");
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Tatiana Nunes', 'tatianan@teste.com.br', 'tatiana321')");
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Tais Fraga', 'taisf@teste.com.br', 'tais456')");
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Gustavo Noia', 'gustavon@teste.com.br', 'gustavo789')");
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES ('Yonatan Farias', 'yonatanf@teste.com.br', 'natan101112')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users");

        }
    }
}
