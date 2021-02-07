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
    [Migration("20200727025136_AddNovelties")]
    partial class AddNovelties
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

                    b.Property<string>("Contact")
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("Phone")
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("Nit")
                        .IsUnique();

                    b.ToTable("Client");
                });

            modelBuilder.Entity("AppLogistics.Objects.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Country");
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

            modelBuilder.Entity("AppLogistics.Objects.EducationLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("EducationLevel");
                });

            modelBuilder.Entity("AppLogistics.Objects.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("AfpId");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<DateTime>("BornDate");

                    b.Property<string>("Comments")
                        .HasMaxLength(512);

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<int>("DocumentTypeId");

                    b.Property<int>("EducationLevelId");

                    b.Property<string>("Email")
                        .HasMaxLength(128);

                    b.Property<string>("EmergencyContact")
                        .HasMaxLength(32);

                    b.Property<string>("EmergencyContactPhone")
                        .HasMaxLength(16);

                    b.Property<int>("EpsId");

                    b.Property<int>("EthnicGroupId");

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

                    b.Property<bool>("HasResidencePermit");

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

                    b.Property<int>("SexId");

                    b.Property<int>("SocialClass");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<bool>("TrainingBASC");

                    b.Property<bool>("TrainingBPM");

                    b.Property<string>("TrainingOthers")
                        .HasMaxLength(512);

                    b.Property<bool>("TrainingSGSST");

                    b.HasKey("Id");

                    b.HasIndex("AfpId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DocumentNumber")
                        .IsUnique();

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("EducationLevelId");

                    b.HasIndex("EpsId");

                    b.HasIndex("EthnicGroupId");

                    b.HasIndex("InternalCode")
                        .IsUnique();

                    b.HasIndex("MaritalStatusId");

                    b.HasIndex("SexId");

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

            modelBuilder.Entity("AppLogistics.Objects.EthnicGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("EthnicGroup");
                });

            modelBuilder.Entity("AppLogistics.Objects.Holding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("EmployeeId");

                    b.Property<decimal>("Price");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Holding");
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

            modelBuilder.Entity("AppLogistics.Objects.Novelty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Novelties");
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

                    b.Property<int?>("ProductId");

                    b.Property<bool>("SplitFare");

                    b.Property<int?>("VehicleTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ClientId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProductId");

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

            modelBuilder.Entity("AppLogistics.Objects.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Sector");
                });

            modelBuilder.Entity("AppLogistics.Objects.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarrierId");

                    b.Property<string>("Comments")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("CustomsInformation")
                        .HasMaxLength(32);

                    b.Property<decimal>("FullPrice");

                    b.Property<decimal>("HoldingPrice");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<int>("Quantity");

                    b.Property<int>("RateId");

                    b.Property<int?>("SectorId");

                    b.Property<string>("VehicleNumber")
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.HasIndex("RateId");

                    b.HasIndex("SectorId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("AppLogistics.Objects.ServiceNovelty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("NoveltyId");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("NoveltyId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceNovelties");
                });

            modelBuilder.Entity("AppLogistics.Objects.Sex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("Sex");
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

            modelBuilder.Entity("AppLogistics.Objects.Employee", b =>
                {
                    b.HasOne("AppLogistics.Objects.Afp", "Afp")
                        .WithMany("Employees")
                        .HasForeignKey("AfpId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Country", "Country")
                        .WithMany("Employees")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.DocumentType", "DocumentType")
                        .WithMany("Employees")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.EducationLevel", "EducationLevel")
                        .WithMany("Employees")
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Eps", "Eps")
                        .WithMany("Employees")
                        .HasForeignKey("EpsId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.EthnicGroup", "EthnicGroup")
                        .WithMany("Employees")
                        .HasForeignKey("EthnicGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.MaritalStatus", "MaritalStatus")
                        .WithMany("Employees")
                        .HasForeignKey("MaritalStatusId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Sex", "Sex")
                        .WithMany("Employees")
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AppLogistics.Objects.Holding", b =>
                {
                    b.HasOne("AppLogistics.Objects.Employee", "Employee")
                        .WithMany("Holdings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Service", "Service")
                        .WithMany("Holdings")
                        .HasForeignKey("ServiceId")
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

                    b.HasOne("AppLogistics.Objects.Product", "Product")
                        .WithMany("Rates")
                        .HasForeignKey("ProductId")
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

            modelBuilder.Entity("AppLogistics.Objects.Service", b =>
                {
                    b.HasOne("AppLogistics.Objects.Carrier", "Carrier")
                        .WithMany("Services")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Rate", "Rate")
                        .WithMany("Services")
                        .HasForeignKey("RateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Sector", "Sector")
                        .WithMany("Services")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AppLogistics.Objects.ServiceNovelty", b =>
                {
                    b.HasOne("AppLogistics.Objects.Novelty", "Novelty")
                        .WithMany("ServiceNovelties")
                        .HasForeignKey("NoveltyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AppLogistics.Objects.Service", "Service")
                        .WithMany("ServiceNovelties")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
