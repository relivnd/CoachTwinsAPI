﻿// <auto-generated />
using System;
using CoachTwinsApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoachTwinsApi.Db.Migrations
{
    [DbContext(typeof(CoachTwinsDbContext))]
    [Migration("20220331104702_TomTest9")]
    partial class TomTest9
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Accepted")
                        .HasColumnType("bit");

                    b.Property<bool>("Canceled")
                        .HasColumnType("bit");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LastChangeSeenByReceiver")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MatchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MatchId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.AuthToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("ActiveGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValidThru")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MatchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UnseenMessagesCoach")
                        .HasColumnType("int");

                    b.Property<int>("UnseenMessagesCoachee")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Criteria", b =>
                {
                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CriteriaEvaluationType")
                        .HasColumnType("int");

                    b.HasKey("Category");

                    b.ToTable("Criteria");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoachId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("MatchedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("isAMatchForCoach")
                        .HasColumnType("bit");

                    b.Property<bool?>("isAMatchForCoachee")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CoachId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.MatchingCriteria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CriteriaCategory")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Prefer")
                        .HasColumnType("bit");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CriteriaCategory");

                    b.HasIndex("UserId");

                    b.ToTable("MatchingCriteria");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SendAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SourceId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.ProfilePicture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("data")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("ProfilePicture");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivacyPolicyAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProfileSetup")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousEducation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfilePictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StartingYear")
                        .HasColumnType("int");

                    b.Property<string>("UniversityProgram")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Coach", b =>
                {
                    b.HasBaseType("CoachTwinsApi.Db.Entities.User");

                    b.Property<bool>("AvailableForMatching")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMatchingProfileSetup")
                        .HasColumnType("bit");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Student", b =>
                {
                    b.HasBaseType("CoachTwinsApi.Db.Entities.User");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Appointment", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("CoachTwinsApi.Db.Entities.Match", "Match")
                        .WithMany("Appointments")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Chat", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.Match", "Match")
                        .WithOne("Chat")
                        .HasForeignKey("CoachTwinsApi.Db.Entities.Chat", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Match", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.Coach", "Coach")
                        .WithMany("Matches")
                        .HasForeignKey("CoachId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoachTwinsApi.Db.Entities.Student", "Student")
                        .WithOne("Match")
                        .HasForeignKey("CoachTwinsApi.Db.Entities.Match", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.MatchingCriteria", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.Criteria", "Criteria")
                        .WithMany("MatchingCriteria")
                        .HasForeignKey("CriteriaCategory");

                    b.HasOne("CoachTwinsApi.Db.Entities.User", "User")
                        .WithMany("MatchingCriteria")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Criteria");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Message", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoachTwinsApi.Db.Entities.User", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");

                    b.Navigation("Chat");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.User", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.ProfilePicture", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Coach", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("CoachTwinsApi.Db.Entities.Coach", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Student", b =>
                {
                    b.HasOne("CoachTwinsApi.Db.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("CoachTwinsApi.Db.Entities.Student", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Criteria", b =>
                {
                    b.Navigation("MatchingCriteria");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Match", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.User", b =>
                {
                    b.Navigation("MatchingCriteria");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Coach", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("CoachTwinsApi.Db.Entities.Student", b =>
                {
                    b.Navigation("Match");
                });
#pragma warning restore 612, 618
        }
    }
}
