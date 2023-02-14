using Microsoft.EntityFrameworkCore;
//using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wt.basic.db.DBModels;

namespace wt.basic.db.DBContexts
{
    public class BasicContext : DbContext
    {
        public BasicContext()
        {

        }

        public BasicContext(DbContextOptions<BasicContext> options)
            : base(options)
        {

        }

        public DbSet<Tb_users> tb_user { get; set; }
        public DbSet<Tb_ppt> tb_ppt { get; set; }
        public DbSet<Tb_picture> tb_picture { get; set; }
        public DbSet<Tb_type> tb_type { get; set; }
        public DbSet<Tb_tags> tb_tags { get; set; }
        public DbSet<Tb_tagPPt> tb_tagppt { get; set; }
        public DbSet<Tb_userPPt_Favr> tb_userppt_fvrt { get; set; }
        public DbSet<Tb_userPPt_Down> tb_userppt_down { get; set; }
        public DbSet<Tb_advice> tb_advice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=10.119.108.234;database=test123;user=root;password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ppt表

            modelBuilder.Entity<Tb_ppt>(entity =>
            {
                entity.ToTable("tb_ppt").HasComment("ppt表").HasCharSet("utf8");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.MinPicture).IsRequired().HasColumnType("varchar(200)");
                entity.Property(e => e.Download_path).IsRequired().HasColumnType("varchar(200)");
                entity.Property(e => e.Size).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.FavrState).IsRequired().HasDefaultValue(false);
            });

            #endregion

            #region 用户表

            modelBuilder.Entity<Tb_users>(entity =>
            {
                entity.ToTable("tb_users").HasComment("用户表").HasCharSet("utf8");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Headshot).IsRequired().HasColumnType("varchar(200)");
                entity.Property(e => e.Name).HasColumnType("varchar(50)");
                entity.Property(e => e.AccountName).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Password).HasColumnType("varchar(50)");
                entity.Property(e => e.Telephone).HasColumnType("varchar(20)");
            });

            #endregion

            #region 文件标签表

            modelBuilder.Entity<Tb_tags>(entity =>
            {
                entity.ToTable("tb_tags").HasComment("ppt标签表").HasCharSet("utf8");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Sort).HasColumnType("int(11)");
                //entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                //entity.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");
            });

            #endregion

            #region 图片

            modelBuilder.Entity<Tb_picture>(entity =>
            {
                entity.ToTable("tb_picture").HasComment("轮播图表").HasCharSet("utf8");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Path).IsRequired().HasColumnType("varchar(200)");
            });

            #endregion

            #region ppt分类表

            modelBuilder.Entity<Tb_type>(entity =>
            {
                entity.ToTable("tb_type").HasComment("ppt分类表").HasCharSet("utf8");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(50)");
                entity.Property(e => e.Sort).HasColumnType("int(11)");
            });

            #endregion

            #region advice建议表

            modelBuilder.Entity<Tb_advice>(entity =>
            {
                entity.ToTable("tb_advice").HasComment("advice建议表").HasCharSet("utf8");
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Advice).IsRequired().HasColumnType("varchar(200)");
                //entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
                entity.Property(e => e.type).IsRequired().HasColumnType("int(1)");
            });

            #endregion

            //entity.Property(e => e.Name).HasColumnType("varchar(50)");

            //定义联合主键
            //modelBuilder.Entity<Tb_favourites>().HasKey(p => new { p.UserId, p.PPTId });

            #region 定义级联关系

            //modelBuilder.Entity<Tb_users>()
            //   .HasMany(u => u.fvrt_ppt)
            //   .WithMany(p => p.fvrt_user)
            //   .UsingEntity(j => j.ToTable("tb_userppt_favourites"));

            //用户ppt多对多收藏表
            modelBuilder.Entity<Tb_users>()
                .HasMany(u => u.fvrt_ppt)
                .WithMany(p => p.fvrt_user)
                .UsingEntity<Tb_userPPt_Favr>(
                j => j.HasOne(p => p.PPt).WithMany(p => p.fvrt_user2).HasForeignKey(pt => pt.PPTId),
                j => j.HasOne(p => p.User).WithMany(p => p.fvrt_ppt2).HasForeignKey(pt => pt.UserId),
                j =>
                {
                    //j.Property(pt => pt.CreateTime).HasDefaultValue(DateTime.Now);
                    j.ToTable("tb_userppt_favourites").HasComment("用户收藏表").HasCharSet("utf8");//.ForMySQLHasCharset("utf8");
                    j.HasKey(t => new { t.UserId, t.PPTId });
                }
                );

            //用户ppt多对多下载表
            modelBuilder.Entity<Tb_users>()
              .HasMany(u => u.down_ppt)
              .WithMany(p => p.down_user)
              .UsingEntity<Tb_userPPt_Down>(
                  j => j.HasOne(p => p.PPt).WithMany(p => p.down_user2).HasForeignKey(pt => pt.PPTId),
                  j => j.HasOne(p => p.User).WithMany(p => p.down_ppt2).HasForeignKey(pt => pt.UserId),
                  j =>
                  {
                      //j.Property(pt => pt.CreateTime).HasDefaultValue(DateTime.Now);
                      j.ToTable("tb_userppt_downloads").HasComment("用户历史下载表").HasCharSet("utf8"); ;
                      j.HasKey(t => new { t.UserId, t.PPTId });
                  }
              );

            //ppt标签多对多
            modelBuilder.Entity<Tb_ppt>()
                .HasMany(p => p.Tag)
                .WithMany(t => t.PPT)
                .UsingEntity<Tb_tagPPt>(
                j => j.HasOne(t => t.Tag).WithMany(p => p.PPT2).HasForeignKey(tp => tp.TagId),
                j => j.HasOne(p => p.PPt).WithMany(p => p.Tag2).HasForeignKey(tp => tp.PPTId),
                j =>
                {
                    j.ToTable("tb_tagppt").HasComment("ppt标签表").HasCharSet("utf8");
                    j.HasKey(t => new { t.PPTId, t.TagId });
                }
                );

            ////用户ppt多对多下载表
            //modelBuilder.Entity<Tb_users>()
            //    .HasMany(u => u.down_ppt)
            //    .WithMany(p => p.down_user)
            //    .UsingEntity(j => j.ToTable("tb_userppt_downloads"));

            //ppt标签一对多关系
            //modelBuilder.Entity<Tb_tags>()
            //    .HasMany(u => u.PPT)
            //    .WithMany(p => p.Tag)
            //    .UsingEntity(j => j.ToTable("tb_tagppt"));

            //ppt用户一对多关系
            modelBuilder.Entity<Tb_ppt>()
                .HasOne(p => p.CreatedBy)
                .WithMany(u => u.pptCreateds);

            //ppt用户一对多关系
            modelBuilder.Entity<Tb_ppt>()
                .HasOne(p => p.ModifiedBy)
                .WithMany(u => u.pptModifys);

            //advice用户一对多关系
            modelBuilder.Entity<Tb_advice>()
               .HasOne(p => p.CreatedBy)
               .WithMany(u => u.adviceCreateds);

            #endregion


        }
    }
}
