using Microsoft.EntityFrameworkCore.Migrations;

namespace MidtermExam.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStudents",
                columns: table => new
                {
                    SubjectStudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    Point = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStudents", x => x.SubjectStudentId);
                    table.ForeignKey(
                        name: "FK_SubjectStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectStudents_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "StudentName" },
                values: new object[,]
                {
                    { 1, "Ana" },
                    { 29, "Giorgi" },
                    { 30, "Ana" },
                    { 31, "Giorgi" },
                    { 32, "Nikoloz" },
                    { 33, "Vaxtang" },
                    { 34, "Ana" },
                    { 35, "Vaxtang" },
                    { 36, "Mari" },
                    { 37, "Mari" },
                    { 38, "Ana" },
                    { 27, "Ana" },
                    { 39, "Eka" },
                    { 41, "Malxaz" },
                    { 42, "Archil" },
                    { 43, "Malxaz" },
                    { 44, "Giorgi" },
                    { 45, "Mari" },
                    { 46, "Mari" },
                    { 47, "Nikoloz" },
                    { 48, "Mari" },
                    { 49, "Giorgi" },
                    { 50, "Archil" },
                    { 40, "Archil" },
                    { 26, "Archil" },
                    { 28, "Nikoloz" },
                    { 24, "Nikoloz" },
                    { 2, "Rakanishu" },
                    { 3, "Ana" },
                    { 4, "Archil" },
                    { 5, "Rakanishu" },
                    { 6, "Mari" },
                    { 7, "Malxaz" },
                    { 8, "Giorgi" },
                    { 9, "Vaxtang" },
                    { 10, "Nikoloz" },
                    { 25, "Malxaz" },
                    { 12, "Archil" },
                    { 11, "Vaxtang" },
                    { 14, "Nikoloz" },
                    { 15, "Nikoloz" },
                    { 16, "Nikoloz" },
                    { 17, "Ana" },
                    { 18, "Malxaz" },
                    { 19, "Rakanishu" },
                    { 20, "Mari" },
                    { 21, "Ana" },
                    { 22, "Rakanishu" },
                    { 23, "Ana" },
                    { 13, "Giorgi" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "SubjectName" },
                values: new object[,]
                {
                    { 4, ".NET" },
                    { 1, "Operating Systems" },
                    { 2, "Security" },
                    { 3, "Databases" },
                    { 5, "Web" }
                });

            migrationBuilder.InsertData(
                table: "SubjectStudents",
                columns: new[] { "SubjectStudentId", "Point", "StudentId", "SubjectId" },
                values: new object[,]
                {
                    { 6, null, 3, 1 },
                    { 34, null, 17, 4 },
                    { 23, null, 12, 4 },
                    { 17, null, 9, 4 },
                    { 14, null, 7, 4 },
                    { 3, null, 2, 4 },
                    { 1, null, 1, 4 },
                    { 99, null, 50, 3 },
                    { 97, null, 49, 3 },
                    { 95, null, 48, 3 },
                    { 87, null, 44, 3 },
                    { 83, null, 42, 3 },
                    { 76, null, 38, 3 },
                    { 60, null, 30, 3 },
                    { 55, null, 28, 3 },
                    { 53, null, 27, 3 },
                    { 52, null, 26, 3 },
                    { 45, null, 23, 3 },
                    { 39, null, 20, 3 },
                    { 31, null, 16, 3 },
                    { 30, null, 15, 3 },
                    { 28, null, 14, 3 },
                    { 37, null, 19, 4 },
                    { 26, null, 13, 3 },
                    { 48, null, 24, 4 },
                    { 57, null, 29, 4 },
                    { 66, null, 33, 5 },
                    { 59, null, 30, 5 },
                    { 43, null, 22, 5 },
                    { 42, null, 21, 5 },
                    { 40, null, 20, 5 },
                    { 16, null, 8, 5 },
                    { 13, null, 7, 5 },
                    { 12, null, 6, 5 },
                    { 10, null, 5, 5 },
                    { 8, null, 4, 5 },
                    { 91, null, 46, 4 },
                    { 90, null, 45, 4 },
                    { 86, null, 43, 4 },
                    { 79, null, 40, 4 },
                    { 77, null, 39, 4 },
                    { 75, null, 38, 4 },
                    { 71, null, 36, 4 },
                    { 69, null, 35, 4 },
                    { 68, null, 34, 4 },
                    { 63, null, 32, 4 },
                    { 62, null, 31, 4 },
                    { 49, null, 25, 4 },
                    { 24, null, 12, 3 },
                    { 18, null, 9, 3 },
                    { 7, null, 4, 3 },
                    { 89, null, 45, 1 },
                    { 81, null, 41, 1 },
                    { 80, null, 40, 1 },
                    { 74, null, 37, 1 },
                    { 72, null, 36, 1 },
                    { 65, null, 33, 1 },
                    { 61, null, 31, 1 },
                    { 54, null, 27, 1 },
                    { 51, null, 26, 1 },
                    { 50, null, 25, 1 },
                    { 47, null, 24, 1 },
                    { 44, null, 22, 1 },
                    { 41, null, 21, 1 },
                    { 38, null, 19, 1 },
                    { 36, null, 18, 1 },
                    { 33, null, 17, 1 },
                    { 32, null, 16, 1 },
                    { 21, null, 11, 1 },
                    { 19, null, 10, 1 },
                    { 15, null, 8, 1 },
                    { 11, null, 6, 1 },
                    { 93, null, 47, 1 },
                    { 96, null, 48, 1 },
                    { 98, null, 49, 1 },
                    { 100, null, 50, 1 },
                    { 94, null, 47, 2 },
                    { 88, null, 44, 2 },
                    { 85, null, 43, 2 },
                    { 84, null, 42, 2 },
                    { 82, null, 41, 2 },
                    { 78, null, 39, 2 },
                    { 73, null, 37, 2 },
                    { 70, null, 35, 2 },
                    { 64, null, 32, 2 },
                    { 58, null, 29, 2 },
                    { 67, null, 34, 5 },
                    { 56, null, 28, 2 },
                    { 35, null, 18, 2 },
                    { 29, null, 15, 2 },
                    { 27, null, 14, 2 },
                    { 25, null, 13, 2 },
                    { 22, null, 11, 2 },
                    { 20, null, 10, 2 },
                    { 9, null, 5, 2 },
                    { 5, null, 3, 2 },
                    { 4, null, 2, 2 },
                    { 2, null, 1, 2 },
                    { 46, null, 23, 2 },
                    { 92, null, 46, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudents_StudentId",
                table: "SubjectStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStudents_SubjectId",
                table: "SubjectStudents",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectStudents");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
