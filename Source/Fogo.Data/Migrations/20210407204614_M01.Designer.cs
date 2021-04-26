﻿// <auto-generated />
using Fogo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fogo.Data.Migrations
{
    [DbContext(typeof(FogoDbContext))]
    [Migration("20210407204614_M01")]
    partial class M01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fogo.Models.AdvertTypeModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("AdvertTypes");
                });

            modelBuilder.Entity("Fogo.Models.AdvertiserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Advertisers");
                });

            modelBuilder.Entity("Fogo.Models.ExecutorModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Executors");
                });

            modelBuilder.Entity("Fogo.Models.ExecutorSocialNetworkModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("ExecutorId")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("SocialNetworkId")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasAlternateKey("ExecutorId", "SocialNetworkId");

                    b.HasIndex("SocialNetworkId");

                    b.ToTable("ExecutorSocialNetworks");
                });

            modelBuilder.Entity("Fogo.Models.RoleModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Fogo.Models.SocialNetworkAdvertTypeModel", b =>
                {
                    b.Property<string>("SocialNetworkId")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("AdvertTypeId")
                        .HasColumnType("varchar(32)");

                    b.HasKey("SocialNetworkId", "AdvertTypeId");

                    b.HasIndex("AdvertTypeId");

                    b.ToTable("SocialNetworkAdvertTypes");
                });

            modelBuilder.Entity("Fogo.Models.SocialNetworkModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("SocialNetworks");
                });

            modelBuilder.Entity("Fogo.Models.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPhoneConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fogo.Models.UserRoleModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(32)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(32)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Fogo.Models.AdvertiserModel", b =>
                {
                    b.HasOne("Fogo.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fogo.Models.ExecutorModel", b =>
                {
                    b.HasOne("Fogo.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fogo.Models.ExecutorSocialNetworkModel", b =>
                {
                    b.HasOne("Fogo.Models.ExecutorModel", "Executor")
                        .WithMany("ExecutorSocialNetworks")
                        .HasForeignKey("ExecutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fogo.Models.SocialNetworkModel", "SocialNetwork")
                        .WithMany()
                        .HasForeignKey("SocialNetworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fogo.Models.SocialNetworkAdvertTypeModel", b =>
                {
                    b.HasOne("Fogo.Models.AdvertTypeModel", "AdvertType")
                        .WithMany()
                        .HasForeignKey("AdvertTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fogo.Models.SocialNetworkModel", "SocialNetwork")
                        .WithMany("SocialNetworkAdvertTypes")
                        .HasForeignKey("SocialNetworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Fogo.Models.UserRoleModel", b =>
                {
                    b.HasOne("Fogo.Models.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fogo.Models.UserModel", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}