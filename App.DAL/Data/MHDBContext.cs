using System;
using System.Collections.Generic;
using App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.DAL.Data
{
    public partial class MHDBContext : DbContext
    {
        public MHDBContext()
        {
        }

        public MHDBContext(DbContextOptions<MHDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAppliedJob> TblAppliedJobs { get; set; } = null!;
        public virtual DbSet<TblCertificate> TblCertificates { get; set; } = null!;
        public virtual DbSet<TblCertificateType> TblCertificateTypes { get; set; } = null!;
        public virtual DbSet<TblCompany> TblCompanies { get; set; } = null!;
        public virtual DbSet<TblCrew> TblCrews { get; set; } = null!;
        public virtual DbSet<TblDesignation> TblDesignations { get; set; } = null!;
        public virtual DbSet<TblDesignationType> TblDesignationTypes { get; set; } = null!;
        public virtual DbSet<TblDirector> TblDirectors { get; set; } = null!;
        public virtual DbSet<TblEducationalBackground> TblEducationalBackgrounds { get; set; } = null!;
        public virtual DbSet<TblExecutive> TblExecutives { get; set; } = null!;
        public virtual DbSet<TblFeedBack> TblFeedBacks { get; set; } = null!;
        public virtual DbSet<TblGalleryPhoto> TblGalleryPhotos { get; set; } = null!;
        public virtual DbSet<TblJob> TblJobs { get; set; } = null!;
        public virtual DbSet<TblLoginHistory> TblLoginHistories { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserRole> TblUserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("server=DESKTOP-OK3LNJL\\SQLEXPRESS; database=MHDB; user id =sres_user; password=badhonrex0007; Trusted_Connection=True");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAppliedJob>(entity =>
            {
                entity.HasKey(e => e.AppliedJobId);

                entity.ToTable("tblAppliedJobs");

                entity.Property(e => e.AppliedJobId).HasColumnName("AppliedJobID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CrewId).HasColumnName("CrewID");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateId)
                    .HasName("PK_tblCertificate_1");

                entity.ToTable("tblCertificate");

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.Property(e => e.CerName).HasMaxLength(100);

                entity.Property(e => e.CerTypeId).HasColumnName("CerTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfIssue).HasColumnType("date");

                entity.Property(e => e.ExpDate).HasColumnType("date");

                entity.Property(e => e.PlaceOfIssue).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            });

            modelBuilder.Entity<TblCertificateType>(entity =>
            {
                entity.HasKey(e => e.CerTypeId)
                    .HasName("PK_tblCertificate");

                entity.ToTable("tblCertificateType");

                entity.Property(e => e.CerTypeId).HasColumnName("CerTypeID");

                entity.Property(e => e.CerTypeName).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("tblCompany");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_1");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_2");

                entity.Property(e => e.Phone3)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_3");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            });

            modelBuilder.Entity<TblCrew>(entity =>
            {
                entity.HasKey(e => e.CrewId);

                entity.ToTable("tblCrew");

                entity.Property(e => e.CrewId).HasColumnName("CrewID");

                entity.Property(e => e.Cdcnumber)
                    .HasMaxLength(50)
                    .HasColumnName("CDCNumber");

                entity.Property(e => e.CrewFirstName).HasMaxLength(50);

                entity.Property(e => e.CrewLastName).HasMaxLength(50);

                entity.Property(e => e.CurrentDesgination).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmergencyContact).HasMaxLength(50);

                entity.Property(e => e.EmrgCntName).HasMaxLength(50);

                entity.Property(e => e.Facebook).HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.Height).HasMaxLength(10);

                entity.Property(e => e.IsOtherCdc).HasColumnName("IsOtherCDC");

                entity.Property(e => e.LinkedIn).HasMaxLength(100);

                entity.Property(e => e.MaritalStatus).HasMaxLength(10);

                entity.Property(e => e.OtherCdcnumber)
                    .HasMaxLength(50)
                    .HasColumnName("OtherCDCNumber");

                entity.Property(e => e.PermanentAddress).HasMaxLength(200);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_1");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_2");

                entity.Property(e => e.PresentAddress).HasMaxLength(200);

                entity.Property(e => e.Relation).HasMaxLength(50);

                entity.Property(e => e.SkypeId)
                    .HasMaxLength(100)
                    .HasColumnName("SkypeID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.Weight).HasMaxLength(10);
                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");


            });

            modelBuilder.Entity<TblDesignation>(entity =>
            {
                entity.HasKey(e => e.DesigId)
                    .HasName("PK_Designation");

                entity.ToTable("tblDesignation");

                entity.Property(e => e.DesigId).HasColumnName("DesigID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DesigName).HasMaxLength(100);

                entity.Property(e => e.DesigTypeId).HasColumnName("DesigTypeID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.DesigType)
                    .WithMany(p => p.TblDesignations)
                    .HasForeignKey(d => d.DesigTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Designation_DesignationType");
            });

            modelBuilder.Entity<TblDesignationType>(entity =>
            {
                entity.HasKey(e => e.DesigTypeId)
                    .HasName("PK_DesignationType");

                entity.ToTable("tblDesignationType");

                entity.Property(e => e.DesigTypeId).HasColumnName("DesigTypeID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DesigName).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDirector>(entity =>
            {
                entity.HasKey(e => e.DirectorId);

                entity.ToTable("tblDirectors");

                entity.Property(e => e.CompanyPost).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasMaxLength(50);

                entity.Property(e => e.DirectorName).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblEducationalBackground>(entity =>
            {
                entity.HasKey(e => e.EduBackId);

                entity.ToTable("tblEducationalBackground");

                entity.Property(e => e.EduBackId).HasColumnName("EduBackID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DegreeName).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.InstitutionName).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            });

            modelBuilder.Entity<TblExecutive>(entity =>
            {
                entity.HasKey(e => e.ExecutiveId);

                entity.ToTable("tblExecutive");

                entity.Property(e => e.ExecutiveId).HasColumnName("ExecutiveID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Designation).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ExFirstName).HasMaxLength(50);

                entity.Property(e => e.ExLastName).HasMaxLength(50);

                entity.Property(e => e.Phone1)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_1");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(50)
                    .HasColumnName("Phone_2");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            });

            modelBuilder.Entity<TblFeedBack>(entity =>
            {
                entity.HasKey(e => e.FeedBackId);

                entity.ToTable("tblFeedBack");

                entity.Property(e => e.FeedBackId).HasColumnName("FeedBackID");

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Opinion).HasMaxLength(50);

                entity.Property(e => e.SenderName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblGalleryPhoto>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.ToTable("tblGalleryPhoto");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Flag).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblJob>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.ToTable("tblJobs");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeadLine).HasColumnType("datetime");

                entity.Property(e => e.Experience).HasMaxLength(50);

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.Property(e => e.JobType).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(100);

                entity.Property(e => e.Salary).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            });

            modelBuilder.Entity<TblLoginHistory>(entity =>
            {
                entity.HasKey(e => e.LogHisId);

                entity.ToTable("tblLoginHistory");

                entity.Property(e => e.LogHisId).HasColumnName("LogHisID");

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblLoginHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLoginHistory_tblUser");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tblUser");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUser_tblUserRole");
            });

            modelBuilder.Entity<TblUserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);

                entity.ToTable("tblUserRole");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserRoleName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
