﻿// <auto-generated />
using System;
using AppLogistics.Data.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppLogistics.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190608232820_AddRate")]
    partial class AddRate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppLogistics.Objects.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Passhash")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("RecoveryToken")
                        .HasMaxLength(36);

                    b.Property<DateTime?>("RecoveryTokenExpirationDate");

                    b.Property<int?>("RoleId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("AppLogistics.Objects.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("AppLogistics.Objects.Afp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.ToTable("Afp");
                });

            modelBuilder.Entity("AppLogistics.Objects.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("Changes")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("EntityId");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("AuditLog");
                });

            modelBuilder.Entity("AppLogistics.Objects.BranchOffice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("BranchOffice");
                });

            modelBuilder.Entity("AppLogistics.Objects.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.ToTable("Carrier");
                });

            modelBuilder.Entity("AppLogistics.Objects.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("BranchOfficeId");

                    b.Property<string>("Contact")
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("Phone")
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("BranchOfficeId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("AppLogistics.Objects.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("AppLogistics.Objects.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("AfpId");

                    b.Property<DateTime>("BornDate");

                    b.Property<int>("BranchOfficeId");

                    b.Property<string>("Comments")
                        .HasMaxLength(512);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<int>("DocumentTypeId");

                    b.Property<string>("Email")
                        .HasMaxLength(128);

                    b.Property<string>("EmergencyContact")
                        .HasMaxLength(32);

                    b.Property<string>("EmergencyContactPhone")
                        .HasMaxLength(16);

                    b.Property<int>("EpsId");

                    b.Property<bool>("HasAdmissionTest");

                    b.Property<bool>("HasContract");

                    b.Property<bool>("HasCurriculumVitae");

                    b.Property<bool>("HasDisciplinaryBackground");

                    b.Property<bool>("HasDocumentCopy");

                    b.Property<bool>("HasEndownmentLetter");

                    b.Property<bool>("HasInternalRegulations");

                    b.Property<bool>("HasKnowledgeTest");

                    b.Property<bool>("HasLaborCertification");

                    b.Property<bool>("HasMilitaryIdCopy");

                    b.Property<bool>("HasPersonalReference");

                    b.Property<bool>("HasPhotos");

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("HomePhone")
                        .HasMaxLength(16);

                    b.Property<string>("InternalCode")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<bool>("IsCriticalPosition");

                    b.Property<int>("MaritalStatusId");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("ResidenceCity")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<DateTime?>("RetirementDate");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("AfpId");

                    b.HasIndex("BranchOfficeId");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("EpsId");

                    b.HasIndex("InternalCode")
                        .IsUnique();

                    b.HasIndex("MaritalStatusId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("AppLogistics.Objects.Eps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.ToTable("Eps");
                });

            modelBuilder.Entity("AppLogistics.Objects.MaritalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("MaritalStatus");
                });

            modelBuilder.Entity("AppLogistics.Objects.Permission", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Area")
                        .HasMaxLength(64);

                    b.Property<string>("Controller")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<DateTime>("CreationDate");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("AppLogistics.Objects.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("AppLogistics.Objects.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityId");

                    b.Property<int>("ClientId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<float>("EmployeePercentage");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<decimal>("Price");

                    b.Property<bool>("SplitFare");

                    b.Property<int?>("VehicleTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ClientId");

                    b.HasIndex("VehicleTypeId");

                    b.ToTable("Rate");
                });

            modelBuilder.Entity("AppLogistics.Objects.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("AppLogistics.Objects.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("AppLogistics.Objects.VehicleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("VehicleType");
                });

            modelBuilder.Entity("AppLogistics.Objects.Account", b =>
                {
                    b.HasOne("AppLogistics.Objects.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AppLogistics.Objects.Client", b =>
                {
                    b.HasOne("AppLogistics.Objects.BranchOffice", "BranchOffice")
                        .WithMany("Clients")
                        .HasForeignKey("BranchOfficeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AppLogistics.Objects.Employee", b =>
                {
                    b.HasOne("AppLogistics.Objects.Afp", "Afp")
                        .WithMany("Employees")
                        .HasForeignKey("AfpId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.BranchOffice", "BranchOffice")
                        .WithMany("Employees")
                        .HasForeignKey("BranchOfficeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.DocumentType", "DocumentType")
                        .WithMany("Employees")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Eps", "Eps")
                        .WithMany("Employees")
                        .HasForeignKey("EpsId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.MaritalStatus", "MaritalStatus")
                        .WithMany("Employees")
                        .HasForeignKey("MaritalStatusId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AppLogistics.Objects.Rate", b =>
                {
                    b.HasOne("AppLogistics.Objects.Activity", "Activity")
                        .WithMany("Rates")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Client", "Client")
                        .WithMany("Rates")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.VehicleType", "VehicleType")
                        .WithMany("Rates")
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AppLogistics.Objects.RolePermission", b =>
                {
                    b.HasOne("AppLogistics.Objects.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Role", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
