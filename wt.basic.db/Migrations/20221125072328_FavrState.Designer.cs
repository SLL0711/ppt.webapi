﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using wt.basic.db.DBContexts;

namespace wt.basic.db.Migrations
{
    [DbContext(typeof(BasicContext))]
    [Migration("20221125072328_FavrState")]
    partial class FavrState
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_picture", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ModifiedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("PptID")
                        .HasColumnType("int");

                    b.Property<int?>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ModifiedByID");

                    b.HasIndex("PptID");

                    b.ToTable("tb_picture");

                    b
                        .HasComment("轮播图表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_ppt", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Download_path")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("FavrState")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("MinPicture")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("ModifiedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Page")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int?>("TypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ModifiedByID");

                    b.HasIndex("TypeID");

                    b.ToTable("tb_ppt");

                    b
                        .HasComment("ppt表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_tagPPt", b =>
                {
                    b.Property<int>("PPTId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PPTId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("tb_tagppt");

                    b
                        .HasComment("ppt标签表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_tags", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ModifiedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Sort")
                        .HasColumnType("int(11)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ModifiedByID");

                    b.ToTable("tb_tags");

                    b
                        .HasComment("ppt标签表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_type", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ModifiedByID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Sort")
                        .HasColumnType("int(11)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.HasIndex("ModifiedByID");

                    b.ToTable("tb_type");

                    b
                        .HasComment("ppt分类表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_userPPt_Down", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PPTId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "PPTId");

                    b.HasIndex("PPTId");

                    b.ToTable("tb_userppt_downloads");

                    b
                        .HasComment("用户历史下载表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_userPPt_Favr", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PPTId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "PPTId");

                    b.HasIndex("PPTId");

                    b.ToTable("tb_userppt_favourites");

                    b
                        .HasComment("用户收藏表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_users", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Headshot")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .HasColumnType("varchar(20)");

                    b.HasKey("ID");

                    b.ToTable("tb_users");

                    b
                        .HasComment("用户表")
                        .HasCharSet("utf8");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_picture", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_users", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("wt.basic.db.DBModels.Tb_users", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");

                    b.HasOne("wt.basic.db.DBModels.Tb_ppt", "Ppt")
                        .WithMany("Turn_picture")
                        .HasForeignKey("PptID");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("Ppt");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_ppt", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_users", "CreatedBy")
                        .WithMany("pptCreateds")
                        .HasForeignKey("CreatedByID");

                    b.HasOne("wt.basic.db.DBModels.Tb_users", "ModifiedBy")
                        .WithMany("pptModifys")
                        .HasForeignKey("ModifiedByID");

                    b.HasOne("wt.basic.db.DBModels.Tb_type", "Type")
                        .WithMany("ppt")
                        .HasForeignKey("TypeID");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_tagPPt", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_ppt", "PPt")
                        .WithMany("Tag2")
                        .HasForeignKey("PPTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("wt.basic.db.DBModels.Tb_tags", "Tag")
                        .WithMany("PPT2")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PPt");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_tags", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_users", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("wt.basic.db.DBModels.Tb_users", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_type", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_users", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID");

                    b.HasOne("wt.basic.db.DBModels.Tb_users", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedByID");

                    b.Navigation("CreatedBy");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_userPPt_Down", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_ppt", "PPt")
                        .WithMany("down_user2")
                        .HasForeignKey("PPTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("wt.basic.db.DBModels.Tb_users", "User")
                        .WithMany("down_ppt2")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PPt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_userPPt_Favr", b =>
                {
                    b.HasOne("wt.basic.db.DBModels.Tb_ppt", "PPt")
                        .WithMany("fvrt_user2")
                        .HasForeignKey("PPTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("wt.basic.db.DBModels.Tb_users", "User")
                        .WithMany("fvrt_ppt2")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PPt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_ppt", b =>
                {
                    b.Navigation("down_user2");

                    b.Navigation("fvrt_user2");

                    b.Navigation("Tag2");

                    b.Navigation("Turn_picture");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_tags", b =>
                {
                    b.Navigation("PPT2");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_type", b =>
                {
                    b.Navigation("ppt");
                });

            modelBuilder.Entity("wt.basic.db.DBModels.Tb_users", b =>
                {
                    b.Navigation("down_ppt2");

                    b.Navigation("fvrt_ppt2");

                    b.Navigation("pptCreateds");

                    b.Navigation("pptModifys");
                });
#pragma warning restore 612, 618
        }
    }
}
