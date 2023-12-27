﻿// <auto-generated />
using System;
using MethodicalSupportDisciplines.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MethodicalSupportDisciplines.Infrastructure.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20231227145332_MethodicalSupportDisciplinesData_Core_RenameFacultyTableProperties")]
    partial class MethodicalSupportDisciplinesData_Core_RenameFacultyTableProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Faculty", b =>
                {
                    b.Property<int>("FacultyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacultyId"));

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FacultyShortName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("FacultyId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.FormatLearning", b =>
                {
                    b.Property<int>("FormatLearningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormatLearningId"));

                    b.Property<string>("FormatLearningName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FormatLearningId");

                    b.ToTable("FormatLearnings");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.LearningStatus", b =>
                {
                    b.Property<int>("LearningStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LearningStatusId"));

                    b.Property<string>("LearningStatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LearningStatusId");

                    b.ToTable("LearningStatuses");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Qualification", b =>
                {
                    b.Property<int>("QualificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QualificationId"));

                    b.Property<string>("QualificationName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("QualificationId");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Specialty", b =>
                {
                    b.Property<int>("SpecialityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecialityId"));

                    b.Property<string>("SpecialityName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("SpecialityNumber")
                        .HasColumnType("int");

                    b.HasKey("SpecialityId");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Discipline", b =>
                {
                    b.Property<int>("DisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DisciplineId"));

                    b.Property<string>("DisciplineName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("DisciplineId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.DisciplineGroup", b =>
                {
                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("DisciplineId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("DisciplineGroups");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<int>("GroupCourse")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.GroupTeacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupTeachers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Mark", b =>
                {
                    b.Property<int>("MarkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MarkId"));

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("MarkValue")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("MarkId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.AdminUser", b =>
                {
                    b.Property<int>("AdminUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminUserId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminUserId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("AdminUsers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.GuestUser", b =>
                {
                    b.Property<int>("GuestUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GuestUserId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GuestUserId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("GuestUsers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.StudentUser", b =>
                {
                    b.Property<int>("StudentUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentUserId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FormatLearningId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("LearningStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("int");

                    b.HasKey("StudentUserId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.HasIndex("FacultyId");

                    b.HasIndex("FormatLearningId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LearningStatusId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("StudentUsers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", b =>
                {
                    b.Property<int>("TeacherUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherUserId"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("QualificationId")
                        .HasColumnType("int");

                    b.HasKey("TeacherUserId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.HasIndex("QualificationId");

                    b.ToTable("TeacherUsers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Discipline", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", "Teacher")
                        .WithMany("Disciplines")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.DisciplineGroup", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Learning.Discipline", "Discipline")
                        .WithMany("DisciplineGroups")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Learning.Group", "Group")
                        .WithMany("DisciplineGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.GroupTeacher", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Learning.Group", "Group")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", "TeacherUser")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("TeacherUser");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Mark", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Learning.Discipline", "Discipline")
                        .WithMany("Marks")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Users.StudentUser", "Student")
                        .WithMany("Marks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", "Teacher")
                        .WithMany("Marks")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.AdminUser", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", "ApplicationUser")
                        .WithOne("AdminUser")
                        .HasForeignKey("MethodicalSupportDisciplines.Core.Entities.Users.AdminUser", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.GuestUser", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", "ApplicationUser")
                        .WithOne("GuestUser")
                        .HasForeignKey("MethodicalSupportDisciplines.Core.Entities.Users.GuestUser", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.StudentUser", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", "ApplicationUser")
                        .WithOne("StudentUser")
                        .HasForeignKey("MethodicalSupportDisciplines.Core.Entities.Users.StudentUser", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Faculty", "Faculty")
                        .WithMany("Students")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.FormatLearning", "FormatLearning")
                        .WithMany("Students")
                        .HasForeignKey("FormatLearningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.Learning.Group", "Group")
                        .WithMany("StudentUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.LearningStatus", "LearningStatus")
                        .WithMany("Students")
                        .HasForeignKey("LearningStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Specialty", "Specialty")
                        .WithMany("Students")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Faculty");

                    b.Navigation("FormatLearning");

                    b.Navigation("Group");

                    b.Navigation("LearningStatus");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", "ApplicationUser")
                        .WithOne("TeacherUser")
                        .HasForeignKey("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Qualification", "Qualification")
                        .WithMany("Teachers")
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Qualification");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Faculty", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.FormatLearning", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.LearningStatus", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Qualification", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.AdditionalLearning.Specialty", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Discipline", b =>
                {
                    b.Navigation("DisciplineGroups");

                    b.Navigation("Marks");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Learning.Group", b =>
                {
                    b.Navigation("DisciplineGroups");

                    b.Navigation("GroupTeachers");

                    b.Navigation("StudentUsers");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.StudentUser", b =>
                {
                    b.Navigation("Marks");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Entities.Users.TeacherUser", b =>
                {
                    b.Navigation("Disciplines");

                    b.Navigation("GroupTeachers");

                    b.Navigation("Marks");
                });

            modelBuilder.Entity("MethodicalSupportDisciplines.Core.Models.Identity.ApplicationUser", b =>
                {
                    b.Navigation("AdminUser");

                    b.Navigation("GuestUser");

                    b.Navigation("StudentUser");

                    b.Navigation("TeacherUser");
                });
#pragma warning restore 612, 618
        }
    }
}
