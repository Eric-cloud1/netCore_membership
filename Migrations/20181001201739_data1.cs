using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemberApi.Migrations
{
    public partial class data1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   

   

            migrationBuilder.DropColumn(
                name: "Strain",
                table: "Product");

     
     
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Product",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ProducerProcessorId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Strain",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DispensaryRegulatoryEntityId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProducerProcessorRegulatoryEntityId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegulatorySystem",
                columns: table => new
                {
                    RegulatorySystemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApiAddress = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatorySystem", x => x.RegulatorySystemId);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryEntity",
                columns: table => new
                {
                    RegulatoryEntityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Approved = table.Column<int>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    RegulatoryApiKey = table.Column<string>(nullable: true),
                    RegulatoryEntityType = table.Column<string>(maxLength: 200, nullable: false),
                    RegulatoryId = table.Column<string>(nullable: true),
                    RegulatoryLoginId = table.Column<string>(nullable: true),
                    RegulatoryState = table.Column<string>(nullable: true),
                    RegulatorySystemId = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryEntity", x => x.RegulatoryEntityId);
                    table.ForeignKey(
                        name: "FK_RegulatoryEntity_RegulatorySystem_RegulatorySystemId",
                        column: x => x.RegulatorySystemId,
                        principalTable: "RegulatorySystem",
                        principalColumn: "RegulatorySystemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegulatoryItem",
                columns: table => new
                {
                    RegulatoryItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegulatoryItemType = table.Column<string>(maxLength: 200, nullable: false),
                    RegulatorySystemId = table.Column<int>(nullable: false),
                    DispensaryId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    TestResultId = table.Column<int>(nullable: true),
                    ActivatedTotal = table.Column<double>(nullable: true),
                    AlphaPinene = table.Column<double>(nullable: true),
                    BetaPinene = table.Column<double>(nullable: true),
                    Caryophyllene = table.Column<double>(nullable: true),
                    Cbc = table.Column<double>(nullable: true),
                    Cbd = table.Column<double>(nullable: true),
                    CbdA = table.Column<double>(nullable: true),
                    CbdTotal = table.Column<double>(nullable: true),
                    Cbg = table.Column<double>(nullable: true),
                    Cbga = table.Column<double>(nullable: true),
                    Cbn = table.Column<double>(nullable: true),
                    DateString = table.Column<string>(nullable: true),
                    Humulene = table.Column<double>(nullable: true),
                    Limonene = table.Column<double>(nullable: true),
                    Linalool = table.Column<double>(nullable: true),
                    Myrcene = table.Column<double>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Ocimene = table.Column<double>(nullable: true),
                    TestResult_ProductId = table.Column<int>(nullable: true),
                    TerpeneTotal = table.Column<double>(nullable: true),
                    Terpinolene = table.Column<double>(nullable: true),
                    TestLab = table.Column<string>(nullable: true),
                    TestingLabId = table.Column<int>(nullable: true),
                    Thc = table.Column<double>(nullable: true),
                    ThcA = table.Column<double>(nullable: true),
                    ThcTotal = table.Column<double>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    producer = table.Column<string>(nullable: true),
                    productType = table.Column<string>(nullable: true),
                    urlLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulatoryItem", x => x.RegulatoryItemId);
                    table.ForeignKey(
                        name: "FK_RegulatoryItem_RegulatoryEntity_DispensaryId",
                        column: x => x.DispensaryId,
                        principalTable: "RegulatoryEntity",
                        principalColumn: "RegulatoryEntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegulatoryItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegulatoryItem_RegulatoryItem_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "RegulatoryItem",
                        principalColumn: "RegulatoryItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegulatoryItem_RegulatorySystem_RegulatorySystemId",
                        column: x => x.RegulatorySystemId,
                        principalTable: "RegulatorySystem",
                        principalColumn: "RegulatorySystemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegulatoryItem_Product_TestResult_ProductId",
                        column: x => x.TestResult_ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegulatoryItem_RegulatoryEntity_TestingLabId",
                        column: x => x.TestingLabId,
                        principalTable: "RegulatoryEntity",
                        principalColumn: "RegulatoryEntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestResultComp",
                columns: table => new
                {
                    TestResultCompId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    imageUrl = table.Column<string>(nullable: true),
                    nonPsychoActiveCosine = table.Column<double>(nullable: false),
                    pscyhoActiveCosine = table.Column<double>(nullable: false),
                    testA_TestResultId = table.Column<int>(nullable: false),
                    testB_TestResultId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultComp", x => x.TestResultCompId);
                    table.ForeignKey(
                        name: "FK_TestResultComp_RegulatoryItem_testA_TestResultId",
                        column: x => x.testA_TestResultId,
                        principalTable: "RegulatoryItem",
                        principalColumn: "RegulatoryItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResultComp_RegulatoryItem_testB_TestResultId",
                        column: x => x.testB_TestResultId,
                        principalTable: "RegulatoryItem",
                        principalColumn: "RegulatoryItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProducerProcessorId",
                table: "Product",
                column: "ProducerProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DispensaryRegulatoryEntityId",
                table: "AspNetUsers",
                column: "DispensaryRegulatoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProducerProcessorRegulatoryEntityId",
                table: "AspNetUsers",
                column: "ProducerProcessorRegulatoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryEntity_RegulatorySystemId",
                table: "RegulatoryEntity",
                column: "RegulatorySystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryItem_DispensaryId",
                table: "RegulatoryItem",
                column: "DispensaryId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryItem_ProductId",
                table: "RegulatoryItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryItem_TestResultId",
                table: "RegulatoryItem",
                column: "TestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryItem_RegulatorySystemId",
                table: "RegulatoryItem",
                column: "RegulatorySystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryItem_TestResult_ProductId",
                table: "RegulatoryItem",
                column: "TestResult_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RegulatoryItem_TestingLabId",
                table: "RegulatoryItem",
                column: "TestingLabId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultComp_testA_TestResultId",
                table: "TestResultComp",
                column: "testA_TestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultComp_testB_TestResultId",
                table: "TestResultComp",
                column: "testB_TestResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegulatoryEntity_ApplicationUserId",
                table: "AspNetUsers",
                column: "ApplicationUserId",
                principalTable: "RegulatoryEntity",
                principalColumn: "RegulatoryEntityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegulatoryEntity_DispensaryRegulatoryEntityId",
                table: "AspNetUsers",
                column: "DispensaryRegulatoryEntityId",
                principalTable: "RegulatoryEntity",
                principalColumn: "RegulatoryEntityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegulatoryEntity_ProducerProcessorRegulatoryEntityId",
                table: "AspNetUsers",
                column: "ProducerProcessorRegulatoryEntityId",
                principalTable: "RegulatoryEntity",
                principalColumn: "RegulatoryEntityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_RegulatoryEntity_ProducerProcessorId",
                table: "Product",
                column: "ProducerProcessorId",
                principalTable: "RegulatoryEntity",
                principalColumn: "RegulatoryEntityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
