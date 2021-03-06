﻿// <auto-generated />
using System;
using AFYAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AFYAPI.Migrations
{
    [DbContext(typeof(AFYContext))]
    partial class AFYContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AFYAPI.Models.AFYEmployee", b =>
                {
                    b.Property<int>("AFYEmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Number");

                    b.Property<int>("UserId");

                    b.HasKey("AFYEmployeeId");

                    b.ToTable("AFYEmployee");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYHoliday", b =>
                {
                    b.Property<int>("AFYHolidayId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Holiday");

                    b.Property<string>("HolidayName");

                    b.HasKey("AFYHolidayId");

                    b.ToTable("AFYHoliday");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYRole", b =>
                {
                    b.Property<int>("AFYRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleIndex");

                    b.Property<string>("RoleName");

                    b.HasKey("AFYRoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYUser", b =>
                {
                    b.Property<int>("AFYUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("AFYUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYUserRole", b =>
                {
                    b.Property<int>("AFYUserRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("RoleAFYRoleId");

                    b.Property<int?>("UserAFYUserId");

                    b.HasKey("AFYUserRoleId");

                    b.HasIndex("RoleAFYRoleId");

                    b.HasIndex("UserAFYUserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYWorkingAccount", b =>
                {
                    b.Property<int>("AFYWorkingAccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmployeeAFYEmployeeId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("HolydaysCount");

                    b.Property<bool>("IsActive");

                    b.Property<decimal>("ShouldWorkingHours");

                    b.Property<decimal>("ShouldWorkingHoursFull");

                    b.Property<DateTime>("StartDate");

                    b.Property<decimal>("WorkingHourPercentage");

                    b.HasKey("AFYWorkingAccountId");

                    b.HasIndex("EmployeeAFYEmployeeId");

                    b.ToTable("AFYWorkingAccount");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYWorkingTime", b =>
                {
                    b.Property<int>("AFYWorkingTimeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountAFYWorkingAccountId");

                    b.Property<decimal>("Difference");

                    b.Property<decimal>("HoursToWork");

                    b.Property<decimal>("HoursWorked");

                    b.Property<int>("Month");

                    b.Property<int>("Week");

                    b.Property<DateTime>("WorkingDay");

                    b.HasKey("AFYWorkingTimeId");

                    b.HasIndex("AccountAFYWorkingAccountId");

                    b.ToTable("AFYWorkingTime");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYUserRole", b =>
                {
                    b.HasOne("AFYAPI.Models.AFYRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleAFYRoleId");

                    b.HasOne("AFYAPI.Models.AFYUser", "User")
                        .WithMany()
                        .HasForeignKey("UserAFYUserId");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYWorkingAccount", b =>
                {
                    b.HasOne("AFYAPI.Models.AFYEmployee", "Employee")
                        .WithMany("Accounts")
                        .HasForeignKey("EmployeeAFYEmployeeId");
                });

            modelBuilder.Entity("AFYAPI.Models.AFYWorkingTime", b =>
                {
                    b.HasOne("AFYAPI.Models.AFYWorkingAccount", "Account")
                        .WithMany("WorkingTimes")
                        .HasForeignKey("AccountAFYWorkingAccountId");
                });
#pragma warning restore 612, 618
        }
    }
}
