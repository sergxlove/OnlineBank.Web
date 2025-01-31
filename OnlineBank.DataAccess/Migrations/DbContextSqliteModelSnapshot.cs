﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBank.DataAccess;

#nullable disable

namespace OnlineBank.DataAccess.Migrations
{
    [DbContext(typeof(DbContextSqlite))]
    partial class DbContextSqliteModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("OnlineBank.DataAccess.Models.CardsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DateEnd")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NumberCard")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("cards", (string)null);
                });

            modelBuilder.Entity("OnlineBank.DataAccess.Models.DataUsersEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("DateBirth")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateRegistration")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NumberPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PassportData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("datausers", (string)null);
                });

            modelBuilder.Entity("OnlineBank.DataAccess.Models.SystemTableEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("NumberCard")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("systemTable", (string)null);
                });

            modelBuilder.Entity("OnlineBank.DataAccess.Models.UsersEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("OnlineBank.DataAccess.Models.CardsEntity", b =>
                {
                    b.HasOne("OnlineBank.DataAccess.Models.UsersEntity", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineBank.DataAccess.Models.DataUsersEntity", b =>
                {
                    b.HasOne("OnlineBank.DataAccess.Models.UsersEntity", "Users")
                        .WithOne("DataUsers")
                        .HasForeignKey("OnlineBank.DataAccess.Models.DataUsersEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OnlineBank.DataAccess.Models.UsersEntity", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("DataUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
