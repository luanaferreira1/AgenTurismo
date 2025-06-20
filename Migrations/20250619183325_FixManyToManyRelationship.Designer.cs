﻿// <auto-generated />
using System;
using AgenTurismo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgenTurismo.Migrations
{
    [DbContext(typeof(AgenciaContext))]
    [Migration("20250619183325_FixManyToManyRelationship")]
    partial class FixManyToManyRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("AgenTurismo.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("AgenTurismo.Models.Destino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Destinos");
                });

            modelBuilder.Entity("AgenTurismo.Models.PacoteTuristico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CapacidadeMaxima")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PacotesTuristicos");
                });

            modelBuilder.Entity("AgenTurismo.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataReserva")
                        .HasColumnType("TEXT");

                    b.Property<int>("PacoteTuristicoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PacoteTuristicoId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("DestinoPacoteTuristico", b =>
                {
                    b.Property<int>("DestinosId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PacotesTuristicosId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DestinosId", "PacotesTuristicosId");

                    b.HasIndex("PacotesTuristicosId");

                    b.ToTable("PacoteDestinos", (string)null);
                });

            modelBuilder.Entity("AgenTurismo.Models.Reserva", b =>
                {
                    b.HasOne("AgenTurismo.Models.Cliente", "Cliente")
                        .WithMany("Reservas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgenTurismo.Models.PacoteTuristico", "PacoteTuristico")
                        .WithMany("Reservas")
                        .HasForeignKey("PacoteTuristicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("PacoteTuristico");
                });

            modelBuilder.Entity("DestinoPacoteTuristico", b =>
                {
                    b.HasOne("AgenTurismo.Models.Destino", null)
                        .WithMany()
                        .HasForeignKey("DestinosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgenTurismo.Models.PacoteTuristico", null)
                        .WithMany()
                        .HasForeignKey("PacotesTuristicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgenTurismo.Models.Cliente", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("AgenTurismo.Models.PacoteTuristico", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}
