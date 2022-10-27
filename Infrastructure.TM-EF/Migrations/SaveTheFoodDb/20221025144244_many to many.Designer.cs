﻿// <auto-generated />
using Infrastructure.TM_EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.TM_EF.Migrations.SaveTheFoodDb
{
    [DbContext(typeof(SaveTheFoodDbContext))]
    [Migration("20221025144244_many to many")]
    partial class manytomany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Kantine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WarmeMaaltijden")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Kantines");
                });

            modelBuilder.Entity("Domain.Medewerker", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LocatieId")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersoneelsNummer")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.HasIndex("LocatieId");

                    b.ToTable("Medewerkers");
                });

            modelBuilder.Entity("Domain.Pakket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AchtienPlus")
                        .HasColumnType("bit");

                    b.Property<string>("DatumTijd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GereserveerdDoorEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OphaalTijd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prijs")
                        .HasColumnType("float");

                    b.Property<string>("TypeMaaltijd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("kantineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GereserveerdDoorEmail");

                    b.HasIndex("kantineId");

                    b.ToTable("Pakketten");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AlcoholHoudend")
                        .HasColumnType("bit");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Producten");
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GeboorteDatum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentNummer")
                        .HasColumnType("int");

                    b.Property<string>("StudieStad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefoonNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Studenten");
                });

            modelBuilder.Entity("PakketProduct", b =>
                {
                    b.Property<int>("ProductenId")
                        .HasColumnType("int");

                    b.Property<int>("pakkettenId")
                        .HasColumnType("int");

                    b.HasKey("ProductenId", "pakkettenId");

                    b.HasIndex("pakkettenId");

                    b.ToTable("PakketProduct");
                });

            modelBuilder.Entity("Domain.Medewerker", b =>
                {
                    b.HasOne("Domain.Kantine", "Locatie")
                        .WithMany()
                        .HasForeignKey("LocatieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locatie");
                });

            modelBuilder.Entity("Domain.Pakket", b =>
                {
                    b.HasOne("Domain.Student", "GereserveerdDoor")
                        .WithMany()
                        .HasForeignKey("GereserveerdDoorEmail");

                    b.HasOne("Domain.Kantine", "kantine")
                        .WithMany()
                        .HasForeignKey("kantineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GereserveerdDoor");

                    b.Navigation("kantine");
                });

            modelBuilder.Entity("PakketProduct", b =>
                {
                    b.HasOne("Domain.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pakket", null)
                        .WithMany()
                        .HasForeignKey("pakkettenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
