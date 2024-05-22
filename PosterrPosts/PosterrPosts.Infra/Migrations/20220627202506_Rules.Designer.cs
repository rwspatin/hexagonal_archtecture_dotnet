﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PosterrPosts.Infra;

#nullable disable

namespace PosterrPosts.Infra.Migrations
{
    [DbContext(typeof(PosterrPostDbContext))]
    [Migration("20220627202506_Rules")]
    partial class Rules
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PosterrPosts.Domain.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID_POST");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PostId")
                        .HasColumnType("integer")
                        .HasColumnName("POST_RELATED_ID");

                    b.Property<string>("PostText")
                        .IsRequired()
                        .HasMaxLength(777)
                        .HasColumnType("character varying(777)")
                        .HasColumnName("POST_TEXT");

                    b.Property<int>("PostType")
                        .HasColumnType("integer")
                        .HasColumnName("POST_TYPE");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("TB_POST", (string)null);
                });

            modelBuilder.Entity("PosterrPosts.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID_USER");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_DATE");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("USER_NAME");

                    b.HasKey("Id");

                    b.ToTable("TB_USER", (string)null);
                });

            modelBuilder.Entity("PosterrPosts.Domain.Entities.Post", b =>
                {
                    b.HasOne("PosterrPosts.Domain.Entities.Post", null)
                        .WithMany("SubPosts")
                        .HasForeignKey("PostId");

                    b.HasOne("PosterrPosts.Domain.Entities.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PosterrPosts.Domain.Entities.Post", b =>
                {
                    b.Navigation("SubPosts");
                });

            modelBuilder.Entity("PosterrPosts.Domain.Entities.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
