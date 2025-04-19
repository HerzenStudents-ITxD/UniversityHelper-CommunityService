﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniversityHelper.CommunityService.Data.Provider.MsSql.Ef;

#nullable disable

namespace UniversityHelper.CommunityService.Data.Provider.MsSql.Ef.Migrations
{
    [DbContext(typeof(CommunityServiceDbContext))]
    partial class CommunityServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbCommunity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Communities", (string)null);
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbCommunityAgent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.ToTable("Agents", (string)null);
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbCommunityHidden", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.ToTable("HiddenCommunities", (string)null);
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbNews", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PointId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.ToTable("News", (string)null);
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbNewsPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NewsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsPhoto", (string)null);
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbParticipating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NewsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Participating", (string)null);
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbCommunityAgent", b =>
                {
                    b.HasOne("UniversityHelper.CommunityService.Models.Db.DbCommunity", "Community")
                        .WithMany("Agents")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbCommunityHidden", b =>
                {
                    b.HasOne("UniversityHelper.CommunityService.Models.Db.DbCommunity", "Community")
                        .WithMany("Hidden")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbNews", b =>
                {
                    b.HasOne("UniversityHelper.CommunityService.Models.Db.DbCommunity", "Community")
                        .WithMany("News")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbNewsPhoto", b =>
                {
                    b.HasOne("UniversityHelper.CommunityService.Models.Db.DbNews", "News")
                        .WithMany("Photos")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbParticipating", b =>
                {
                    b.HasOne("UniversityHelper.CommunityService.Models.Db.DbNews", "News")
                        .WithMany("Participatings")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbCommunity", b =>
                {
                    b.Navigation("Agents");

                    b.Navigation("Hidden");

                    b.Navigation("News");
                });

            modelBuilder.Entity("UniversityHelper.CommunityService.Models.Db.DbNews", b =>
                {
                    b.Navigation("Participatings");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
