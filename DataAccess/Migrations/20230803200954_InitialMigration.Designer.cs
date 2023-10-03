﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SealabAPI.DataAccess;

#nullable disable

namespace SealabAPI.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230803200954_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.Assistant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user");

                    b.Property<string>("Position")
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.HasKey("Id")
                        .HasName("pk_assistant");

                    b.HasIndex("IdUser")
                        .HasDatabaseName("ix_assistant_id_user");

                    b.ToTable("assistant", (string)null);
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.PreliminaryAssignmentAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Answer")
                        .HasColumnType("text")
                        .HasColumnName("answer");

                    b.Property<Guid>("IdQuestion")
                        .HasColumnType("uuid")
                        .HasColumnName("id_question");

                    b.Property<Guid>("IdStudent")
                        .HasColumnType("uuid")
                        .HasColumnName("id_student");

                    b.HasKey("Id")
                        .HasName("pk_preliminary_assignment_answer");

                    b.HasIndex("IdQuestion")
                        .HasDatabaseName("ix_preliminary_assignment_answer_id_question");

                    b.HasIndex("IdStudent")
                        .HasDatabaseName("ix_preliminary_assignment_answer_id_student");

                    b.ToTable("preliminary_assignment_answer", (string)null);
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.PreliminaryAssignmentQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AnswerKey")
                        .HasColumnType("text")
                        .HasColumnName("answer_key");

                    b.Property<string>("FilePath")
                        .HasColumnType("text")
                        .HasColumnName("file_path");

                    b.Property<int>("Module")
                        .HasColumnType("integer")
                        .HasColumnName("module");

                    b.Property<string>("Question")
                        .HasColumnType("text")
                        .HasColumnName("question");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_preliminary_assignment_question");

                    b.ToTable("preliminary_assignment_question", (string)null);
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Classroom")
                        .HasColumnType("text")
                        .HasColumnName("classroom");

                    b.Property<int>("Day")
                        .HasColumnType("integer")
                        .HasColumnName("day");

                    b.Property<int>("Group")
                        .HasColumnType("integer")
                        .HasColumnName("group");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("id_user");

                    b.Property<int>("Shift")
                        .HasColumnType("integer")
                        .HasColumnName("shift");

                    b.HasKey("Id")
                        .HasName("pk_student");

                    b.HasIndex("IdUser")
                        .HasDatabaseName("ix_student_id_user");

                    b.ToTable("student", (string)null);
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AppToken")
                        .HasColumnType("text")
                        .HasColumnName("app_token");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Nim")
                        .HasColumnType("text")
                        .HasColumnName("nim");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.Assistant", b =>
                {
                    b.HasOne("SealabAPI.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_assistant_user_id_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.PreliminaryAssignmentAnswer", b =>
                {
                    b.HasOne("SealabAPI.DataAccess.Entities.PreliminaryAssignmentQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("IdQuestion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_preliminary_assignment_answer_preliminary_assignment_questi");

                    b.HasOne("SealabAPI.DataAccess.Entities.Student", "Student")
                        .WithMany("PreliminaryAssignments")
                        .HasForeignKey("IdStudent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_preliminary_assignment_answer_student_id_student");

                    b.Navigation("Question");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.Student", b =>
                {
                    b.HasOne("SealabAPI.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_student_user_id_user");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SealabAPI.DataAccess.Entities.Student", b =>
                {
                    b.Navigation("PreliminaryAssignments");
                });
#pragma warning restore 612, 618
        }
    }
}