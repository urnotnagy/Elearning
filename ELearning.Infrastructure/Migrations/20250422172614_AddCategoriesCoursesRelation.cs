using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ELearning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesCoursesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCourse_Categories_CategoriesId",
                table: "CategoryCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCourse_Courses_CoursesId",
                table: "CategoryCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryCourse",
                table: "CategoryCourse");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("134ebc4e-a4e0-4a45-a671-a744c2158b3a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6766040e-ee81-4bd7-b8fb-c63e379f7861"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a53d9859-d952-46b5-aff6-bdda03818759"));

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { new Guid("c33c8a22-4def-4f4e-8b80-9d04f109b0b3"), new Guid("c8e303da-66dc-4367-baa3-fd4f02a740ef") });

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("04c21d93-5e7d-4a5a-bdbe-c67cff58dc1f"));

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: new Guid("4f789e66-944c-42a8-b1d3-92e673745ca9"));

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: new Guid("5b9d2bbf-f779-4587-a2de-a2ad125f38db"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("6e7f7847-a314-45d8-8f16-c1748a858d58"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c8e303da-66dc-4367-baa3-fd4f02a740ef"));

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: new Guid("fd2f96eb-af61-4487-ac50-0855c18dbefc"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("f84a5d98-5e7d-4961-9bd8-1c4631edd4df"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c33c8a22-4def-4f4e-8b80-9d04f109b0b3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9454cf5f-982f-4b84-a4ed-71fedb072162"));

            migrationBuilder.RenameTable(
                name: "CategoryCourse",
                newName: "CourseCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryCourse_CoursesId",
                table: "CourseCategories",
                newName: "IX_CourseCategories_CoursesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCategories",
                table: "CourseCategories",
                columns: new[] { "CategoriesId", "CoursesId" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IconUrl", "IsDeleted", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("23e2576c-274c-4da8-9dc9-99649fcf8957"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6271), "Learn programming languages and software development", "https://example.com/icons/programming.png", false, "Programming", null, null },
                    { new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6301), "Learn data science and analytics", "https://example.com/icons/data-science.png", false, "Data Science", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "CreatedAt", "Email", "FirstName", "IsActive", "IsDeleted", "LastLoginAt", "LastName", "PasswordHash", "ProfilePictureUrl", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), null, new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6502), "instructor@example.com", "John", true, false, null, "Doe", "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", null, 1, null },
                    { new Guid("76ebfe4b-4856-4363-856e-4bd8ca3ec4be"), null, new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6511), "student@example.com", "Jane", true, false, null, "Smith", "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IconUrl", "IsDeleted", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("6dd69128-e3e1-45ae-8bde-267d6f8849ac"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6321), "Learn machine learning algorithms and techniques", "https://example.com/icons/machine-learning.png", false, "Machine Learning", new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), null },
                    { new Guid("6e3e1a0a-b9ea-4183-bda2-06e8b406ff7e"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6367), "Learn big data processing and analytics", "https://example.com/icons/big-data.png", false, "Big Data", new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), null },
                    { new Guid("71f4cb4b-1ec6-4803-8b73-ca3dca27857e"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6317), "Learn mobile app development for iOS and Android", "https://example.com/icons/mobile-dev.png", false, "Mobile Development", new Guid("23e2576c-274c-4da8-9dc9-99649fcf8957"), null },
                    { new Guid("e10f510e-7c64-473b-bdaf-f172e8c71e5e"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6293), "Learn web development technologies", "https://example.com/icons/web-dev.png", false, "Web Development", new Guid("23e2576c-274c-4da8-9dc9-99649fcf8957"), null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationInWeeks", "ImageUrl", "InstructorId", "IsDeleted", "IsPublished", "Level", "Price", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6592), "Learn the fundamentals of C# programming", 8, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 0, 49.99m, 1, "Introduction to C#", null },
                    { new Guid("5404eecb-f375-4f20-9826-af9f6f59f0e6"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6633), "Learn Flutter for building cross-platform mobile apps", 12, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 1, 89.99m, 1, "Flutter Mobile App Development", null },
                    { new Guid("6527b9a8-8da9-402c-97ee-5c4d30d6a3cd"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6601), "Learn HTML, CSS, and JavaScript for web development", 10, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 0, 59.99m, 1, "Web Development Fundamentals", null },
                    { new Guid("84fd87c3-8e35-453c-8814-5eed1ddde358"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6622), "Learn Node.js for building server-side applications", 8, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 1, 79.99m, 1, "Node.js Backend Development", null },
                    { new Guid("9bba6c5c-8642-4bf6-a0dd-c017c002ba73"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6639), "Learn the fundamentals of machine learning", 14, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 2, 99.99m, 1, "Machine Learning Fundamentals", null },
                    { new Guid("bdbf0171-3513-4bfc-b69b-98291fec6d68"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6628), "Learn Python programming for data science", 10, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 0, 59.99m, 1, "Python for Data Science", null },
                    { new Guid("c06d725c-247e-4e44-99d0-4f89f1f6ea5c"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6608), "Learn the fundamentals of data science and analytics", 12, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 1, 69.99m, 1, "Data Science Essentials", null },
                    { new Guid("c6994240-f166-403f-a81a-2cccf2da90bd"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6614), "Learn React.js for building modern web applications", 8, null, new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, 1, 79.99m, 1, "React.js for Frontend Development", null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IconUrl", "IsDeleted", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("671a8cf6-5719-470c-93f4-955dd934e018"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6305), "Learn frontend technologies like HTML, CSS, and JavaScript", "https://example.com/icons/frontend.png", false, "Frontend Development", new Guid("e10f510e-7c64-473b-bdaf-f172e8c71e5e"), null },
                    { new Guid("ba15c181-cab4-4cc0-a6f2-9d6fa3aae9c2"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(6309), "Learn backend technologies and server-side programming", "https://example.com/icons/backend.png", false, "Backend Development", new Guid("e10f510e-7c64-473b-bdaf-f172e8c71e5e"), null }
                });

            migrationBuilder.InsertData(
                table: "CourseCategories",
                columns: new[] { "CategoriesId", "CoursesId" },
                values: new object[,]
                {
                    { new Guid("23e2576c-274c-4da8-9dc9-99649fcf8957"), new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c") },
                    { new Guid("6dd69128-e3e1-45ae-8bde-267d6f8849ac"), new Guid("9bba6c5c-8642-4bf6-a0dd-c017c002ba73") },
                    { new Guid("71f4cb4b-1ec6-4803-8b73-ca3dca27857e"), new Guid("5404eecb-f375-4f20-9826-af9f6f59f0e6") },
                    { new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), new Guid("bdbf0171-3513-4bfc-b69b-98291fec6d68") },
                    { new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), new Guid("c06d725c-247e-4e44-99d0-4f89f1f6ea5c") },
                    { new Guid("e10f510e-7c64-473b-bdaf-f172e8c71e5e"), new Guid("6527b9a8-8da9-402c-97ee-5c4d30d6a3cd") }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "CompletedAt", "CreatedAt", "EnrolledAt", "Grade", "Id", "IsDeleted", "UpdatedAt" },
                values: new object[] { new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c"), new Guid("76ebfe4b-4856-4363-856e-4bd8ca3ec4be"), null, new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7419), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7418), null, new Guid("ef8de6c1-7fb8-4a1b-9df4-57609bb4e714"), false, null });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "IsDeleted", "Order", "Title", "UpdatedAt" },
                values: new object[] { new Guid("cfb2f59e-36c4-4ce5-8bcb-1ece58aecaa3"), new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7122), "Learn the basics of C# syntax and programming concepts", false, 1, "Getting Started with C#", null });

            migrationBuilder.InsertData(
                table: "CourseCategories",
                columns: new[] { "CategoriesId", "CoursesId" },
                values: new object[,]
                {
                    { new Guid("671a8cf6-5719-470c-93f4-955dd934e018"), new Guid("c6994240-f166-403f-a81a-2cccf2da90bd") },
                    { new Guid("ba15c181-cab4-4cc0-a6f2-9d6fa3aae9c2"), new Guid("84fd87c3-8e35-453c-8814-5eed1ddde358") }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "CreatedAt", "Description", "DurationInMinutes", "IsDeleted", "ModuleId", "Order", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { new Guid("2eb373ae-f901-4b44-be20-ca4961665a83"), "In this lesson, we'll learn about variables and data types...", new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7184), "Understanding variables and data types in C#", 30, false, new Guid("cfb2f59e-36c4-4ce5-8bcb-1ece58aecaa3"), 1, "Variables and Data Types", null, null });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CreatedAt", "Description", "InstructorId", "IsDeleted", "IsPublished", "ModuleId", "PassingScore", "TimeLimitInMinutes", "Title", "UpdatedAt" },
                values: new object[] { new Guid("dd74619d-5b56-4446-be8a-409b79c316b3"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7248), "Test your knowledge of C# basics", new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"), false, true, new Guid("cfb2f59e-36c4-4ce5-8bcb-1ece58aecaa3"), 70, 30, "C# Basics Quiz", null });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Points", "QuizId", "Text", "Type", "UpdatedAt" },
                values: new object[] { new Guid("7bde69dd-cd48-4dc6-aeff-15ede85ba071"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7303), false, 10, new Guid("dd74619d-5b56-4446-be8a-409b79c316b3"), "What is a variable in C#?", 0, null });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "IsDeleted", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("94327b86-838c-4bea-a857-4f09dff028a8"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7354), true, false, new Guid("7bde69dd-cd48-4dc6-aeff-15ede85ba071"), "A container for storing data", null },
                    { new Guid("f3f12085-bec1-4dbd-b42d-ca99f36051b4"), new DateTime(2025, 4, 22, 17, 26, 13, 682, DateTimeKind.Utc).AddTicks(7359), false, false, new Guid("7bde69dd-cd48-4dc6-aeff-15ede85ba071"), "A method in C#", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_Categories_CategoriesId",
                table: "CourseCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_Courses_CoursesId",
                table: "CourseCategories",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_Categories_CategoriesId",
                table: "CourseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_Courses_CoursesId",
                table: "CourseCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCategories",
                table: "CourseCategories");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6e3e1a0a-b9ea-4183-bda2-06e8b406ff7e"));

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("23e2576c-274c-4da8-9dc9-99649fcf8957"), new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("671a8cf6-5719-470c-93f4-955dd934e018"), new Guid("c6994240-f166-403f-a81a-2cccf2da90bd") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("6dd69128-e3e1-45ae-8bde-267d6f8849ac"), new Guid("9bba6c5c-8642-4bf6-a0dd-c017c002ba73") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("71f4cb4b-1ec6-4803-8b73-ca3dca27857e"), new Guid("5404eecb-f375-4f20-9826-af9f6f59f0e6") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("ba15c181-cab4-4cc0-a6f2-9d6fa3aae9c2"), new Guid("84fd87c3-8e35-453c-8814-5eed1ddde358") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), new Guid("bdbf0171-3513-4bfc-b69b-98291fec6d68") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"), new Guid("c06d725c-247e-4e44-99d0-4f89f1f6ea5c") });

            migrationBuilder.DeleteData(
                table: "CourseCategories",
                keyColumns: new[] { "CategoriesId", "CoursesId" },
                keyValues: new object[] { new Guid("e10f510e-7c64-473b-bdaf-f172e8c71e5e"), new Guid("6527b9a8-8da9-402c-97ee-5c4d30d6a3cd") });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c"), new Guid("76ebfe4b-4856-4363-856e-4bd8ca3ec4be") });

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: new Guid("2eb373ae-f901-4b44-be20-ca4961665a83"));

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: new Guid("94327b86-838c-4bea-a857-4f09dff028a8"));

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumn: "Id",
                keyValue: new Guid("f3f12085-bec1-4dbd-b42d-ca99f36051b4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("671a8cf6-5719-470c-93f4-955dd934e018"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6dd69128-e3e1-45ae-8bde-267d6f8849ac"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("71f4cb4b-1ec6-4803-8b73-ca3dca27857e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ba15c181-cab4-4cc0-a6f2-9d6fa3aae9c2"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("5404eecb-f375-4f20-9826-af9f6f59f0e6"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("6527b9a8-8da9-402c-97ee-5c4d30d6a3cd"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("84fd87c3-8e35-453c-8814-5eed1ddde358"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("9bba6c5c-8642-4bf6-a0dd-c017c002ba73"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("bdbf0171-3513-4bfc-b69b-98291fec6d68"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c06d725c-247e-4e44-99d0-4f89f1f6ea5c"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("c6994240-f166-403f-a81a-2cccf2da90bd"));

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("7bde69dd-cd48-4dc6-aeff-15ede85ba071"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76ebfe4b-4856-4363-856e-4bd8ca3ec4be"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d621af3d-6ca5-4ec1-84be-3436dd70d652"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e10f510e-7c64-473b-bdaf-f172e8c71e5e"));

            migrationBuilder.DeleteData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: new Guid("dd74619d-5b56-4446-be8a-409b79c316b3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("23e2576c-274c-4da8-9dc9-99649fcf8957"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cfb2f59e-36c4-4ce5-8bcb-1ece58aecaa3"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("51730bd4-b4e4-458f-b629-f9b349e2430c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("05960a70-06b6-40e0-b18e-e03ddae0e5b5"));

            migrationBuilder.RenameTable(
                name: "CourseCategories",
                newName: "CategoryCourse");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategories_CoursesId",
                table: "CategoryCourse",
                newName: "IX_CategoryCourse_CoursesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryCourse",
                table: "CategoryCourse",
                columns: new[] { "CategoriesId", "CoursesId" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IconUrl", "IsDeleted", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("134ebc4e-a4e0-4a45-a671-a744c2158b3a"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(2792), "Learn data science and analytics", "", false, "Data Science", null, null },
                    { new Guid("6766040e-ee81-4bd7-b8fb-c63e379f7861"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(2745), "Learn programming languages and software development", "", false, "Programming", null, null },
                    { new Guid("a53d9859-d952-46b5-aff6-bdda03818759"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(2767), "Learn web development technologies", "", false, "Web Development", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "CreatedAt", "Email", "FirstName", "IsActive", "IsDeleted", "LastLoginAt", "LastName", "PasswordHash", "ProfilePictureUrl", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("9454cf5f-982f-4b84-a4ed-71fedb072162"), null, new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(2913), "instructor@example.com", "John", true, false, null, "Doe", "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", null, 1, null },
                    { new Guid("c8e303da-66dc-4367-baa3-fd4f02a740ef"), null, new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(2921), "student@example.com", "Jane", true, false, null, "Smith", "AQAAAAIAAYagAAAAELbHJBk5JqrA4j9w9G6h2Q6Y4/UdH0zYZRgT5r4HuPGXWEyEFnxiWNVJJBgZXg==", null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationInWeeks", "ImageUrl", "InstructorId", "IsDeleted", "IsPublished", "Level", "Price", "Status", "Title", "UpdatedAt" },
                values: new object[] { new Guid("c33c8a22-4def-4f4e-8b80-9d04f109b0b3"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(2994), "Learn the fundamentals of C# programming", 0, null, new Guid("9454cf5f-982f-4b84-a4ed-71fedb072162"), false, true, 0, 49.99m, 0, "Introduction to C#", null });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "CompletedAt", "CreatedAt", "EnrolledAt", "Grade", "Id", "IsDeleted", "UpdatedAt" },
                values: new object[] { new Guid("c33c8a22-4def-4f4e-8b80-9d04f109b0b3"), new Guid("c8e303da-66dc-4367-baa3-fd4f02a740ef"), null, new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3317), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3315), null, new Guid("6d77eab9-f799-41d5-a68c-b4ba9b8aec8e"), false, null });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Description", "IsDeleted", "Order", "Title", "UpdatedAt" },
                values: new object[] { new Guid("f84a5d98-5e7d-4961-9bd8-1c4631edd4df"), new Guid("c33c8a22-4def-4f4e-8b80-9d04f109b0b3"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3073), "Learn the basics of C# syntax and programming concepts", false, 1, "Getting Started with C#", null });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "CreatedAt", "Description", "DurationInMinutes", "IsDeleted", "ModuleId", "Order", "Title", "UpdatedAt", "VideoUrl" },
                values: new object[] { new Guid("04c21d93-5e7d-4a5a-bdbe-c67cff58dc1f"), "In this lesson, we'll learn about variables and data types...", new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3126), "Understanding variables and data types in C#", 30, false, new Guid("f84a5d98-5e7d-4961-9bd8-1c4631edd4df"), 1, "Variables and Data Types", null, null });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CreatedAt", "Description", "InstructorId", "IsDeleted", "IsPublished", "ModuleId", "PassingScore", "TimeLimitInMinutes", "Title", "UpdatedAt" },
                values: new object[] { new Guid("fd2f96eb-af61-4487-ac50-0855c18dbefc"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3175), "Test your knowledge of C# basics", new Guid("9454cf5f-982f-4b84-a4ed-71fedb072162"), false, true, new Guid("f84a5d98-5e7d-4961-9bd8-1c4631edd4df"), 70, 30, "C# Basics Quiz", null });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Points", "QuizId", "Text", "Type", "UpdatedAt" },
                values: new object[] { new Guid("6e7f7847-a314-45d8-8f16-c1748a858d58"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3226), false, 10, new Guid("fd2f96eb-af61-4487-ac50-0855c18dbefc"), "What is a variable in C#?", 0, null });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "IsDeleted", "QuestionId", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("4f789e66-944c-42a8-b1d3-92e673745ca9"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3269), true, false, new Guid("6e7f7847-a314-45d8-8f16-c1748a858d58"), "A container for storing data", null },
                    { new Guid("5b9d2bbf-f779-4587-a2de-a2ad125f38db"), new DateTime(2025, 4, 21, 19, 22, 33, 769, DateTimeKind.Utc).AddTicks(3274), false, false, new Guid("6e7f7847-a314-45d8-8f16-c1748a858d58"), "A method in C#", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCourse_Categories_CategoriesId",
                table: "CategoryCourse",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCourse_Courses_CoursesId",
                table: "CategoryCourse",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
