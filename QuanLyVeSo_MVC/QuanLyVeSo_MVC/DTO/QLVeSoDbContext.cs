namespace QuanLyVeSo_MVC.DTO
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLVeSoDbContext : DbContext
    {
        public QLVeSoDbContext()
            : base("name=QLVeSoDbContext")
        {
        }

        public virtual DbSet<CongNo> CongNo { get; set; }
        public virtual DbSet<DaiLy> DaiLy { get; set; }
        public virtual DbSet<Giai> Giai { get; set; }
        public virtual DbSet<KetQuaSoXo> KetQuaSoXo { get; set; }
        public virtual DbSet<LoaiVeso> LoaiVeso { get; set; }
        public virtual DbSet<PhatHanh> PhatHanh { get; set; }
        public virtual DbSet<PhieuChi> PhieuChi { get; set; }
        public virtual DbSet<PhieuThu> PhieuThu { get; set; }
        public virtual DbSet<SoLuongDK> SoLuongDK { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CongNo>()
                .Property(e => e.MaCongNo)
                .IsUnicode(false);

            modelBuilder.Entity<CongNo>()
                .Property(e => e.MaDaiLy)
                .IsUnicode(false);

            modelBuilder.Entity<CongNo>()
                .Property(e => e.SoTienNo)
                .HasPrecision(19, 3);

            modelBuilder.Entity<DaiLy>()
                .Property(e => e.MaDaiLy)
                .IsUnicode(false);

            modelBuilder.Entity<DaiLy>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<Giai>()
                .Property(e => e.MaGiai)
                .IsUnicode(false);

            modelBuilder.Entity<Giai>()
                .Property(e => e.SoTienNhan)
                .HasPrecision(19, 3);

            modelBuilder.Entity<KetQuaSoXo>()
                .Property(e => e.MaKetQua)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaSoXo>()
                .Property(e => e.MaLoaiVeSo)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaSoXo>()
                .Property(e => e.MaGiai)
                .IsUnicode(false);

            modelBuilder.Entity<KetQuaSoXo>()
                .Property(e => e.SoTrung)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiVeso>()
                .Property(e => e.MaLoaiVeSo)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiVeso>()
                .Property(e => e.GiaBan)
                .HasPrecision(19, 3);

            modelBuilder.Entity<PhatHanh>()
                .Property(e => e.MaPhatHanh)
                .IsUnicode(false);

            modelBuilder.Entity<PhatHanh>()
                .Property(e => e.MaDaiLy)
                .IsUnicode(false);

            modelBuilder.Entity<PhatHanh>()
                .Property(e => e.MaLoaiVeSo)
                .IsUnicode(false);

            modelBuilder.Entity<PhatHanh>()
                .Property(e => e.DoanhThuDPH)
                .HasPrecision(19, 3);

            modelBuilder.Entity<PhatHanh>()
                .Property(e => e.HoaHong)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PhatHanh>()
                .Property(e => e.TienThanhToan)
                .HasPrecision(19, 3);

            modelBuilder.Entity<PhieuChi>()
                .Property(e => e.MaPhieuChi)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuChi>()
                .Property(e => e.SoTien)
                .HasPrecision(19, 3);

            modelBuilder.Entity<PhieuThu>()
                .Property(e => e.MaPhieuThu)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuThu>()
                .Property(e => e.MaDaiLy)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuThu>()
                .Property(e => e.SoTienNop)
                .HasPrecision(19, 3);

            modelBuilder.Entity<SoLuongDK>()
                .Property(e => e.MaSoLuongDK)
                .IsUnicode(false);

            modelBuilder.Entity<SoLuongDK>()
                .Property(e => e.MaDaiLy)
                .IsUnicode(false);
            //modelBuilder.Entity<SoLuongDK>().HasKey(i => new { i.MaDaiLy }).ToTable("DaiLy");
        }
    }
}
