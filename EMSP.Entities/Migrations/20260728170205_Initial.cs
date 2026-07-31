using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSP.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bank_name_ar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    bank_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_banks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_name_ar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nationality_ar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    nationality_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country_code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    establishment_name_ar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    establishment_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    establishment_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    establishment_type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    national_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    commercial_registration_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    short_address = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    full_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    vat_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_establishments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    basic_salary = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    transportation_allowance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    housing_allowance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    other_allowance = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    total_salary = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_salaries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_name_ar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    company_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    company_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    short_address = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    full_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contact_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    vat_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    establishment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.id);
                    table.ForeignKey(
                        name: "fk_companies_establishments_establishment_id",
                        column: x => x.establishment_id,
                        principalTable: "Establishments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthInsurances",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    policy_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    insurance_provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    policy_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    establishment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_health_insurances", x => x.id);
                    table.ForeignKey(
                        name: "fk_health_insurances_establishments_establishment_id",
                        column: x => x.establishment_id,
                        principalTable: "Establishments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name_ar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    full_name_en = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    iqama_or_id_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    passport_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    border_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    email_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    passport_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    iqama_or_id_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    country_id = table.Column<Guid>(type: "uuid", nullable: true),
                    profession = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contract_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    hire_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    termination_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    establishment_id = table.Column<Guid>(type: "uuid", nullable: true),
                    company_id = table.Column<Guid>(type: "uuid", nullable: true),
                    iban = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    account_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    unlisted_bank_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    bank_id = table.Column<Guid>(type: "uuid", nullable: true),
                    salary_id = table.Column<Guid>(type: "uuid", nullable: true),
                    member_policy_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    health_insurance_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_banks_bank_id",
                        column: x => x.bank_id,
                        principalTable: "Banks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_employees_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "Companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_employees_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_employees_establishments_establishment_id",
                        column: x => x.establishment_id,
                        principalTable: "Establishments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_employees_health_insurances_health_insurance_id",
                        column: x => x.health_insurance_id,
                        principalTable: "HealthInsurances",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_employees_salaries_salary_id",
                        column: x => x.salary_id,
                        principalTable: "Salaries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCosts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cost_type = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    cost_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_paid = table.Column<bool>(type: "boolean", nullable: false),
                    paid_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reference_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_costs", x => x.id);
                    table.ForeignKey(
                        name: "fk_employee_costs_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "Employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_companies_company_code",
                table: "Companies",
                column: "company_code");

            migrationBuilder.CreateIndex(
                name: "ix_companies_establishment_id",
                table: "Companies",
                column: "establishment_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_costs_employee_id",
                table: "EmployeeCosts",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_bank_id",
                table: "Employees",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_company_id",
                table: "Employees",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_country_id",
                table: "Employees",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_email_address",
                table: "Employees",
                column: "email_address");

            migrationBuilder.CreateIndex(
                name: "ix_employees_establishment_id",
                table: "Employees",
                column: "establishment_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_health_insurance_id",
                table: "Employees",
                column: "health_insurance_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_iqama_or_id_number",
                table: "Employees",
                column: "iqama_or_id_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_employees_salary_id",
                table: "Employees",
                column: "salary_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_establishments_establishment_code",
                table: "Establishments",
                column: "establishment_code");

            migrationBuilder.CreateIndex(
                name: "ix_health_insurances_establishment_id",
                table: "HealthInsurances",
                column: "establishment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCosts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "HealthInsurances");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Establishments");
        }
    }
}
