namespace Modal.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context_ : DbContext
    {
        public Context_()
            : base("name=Context_")
        {
        }

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<DauGia> DauGias { get; set; }
        public virtual DbSet<GHN_Ship> GHN_Ship { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<HoaDonCT> HoaDonCTs { get; set; }
        public virtual DbSet<LichSuDG> LichSuDGs { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<LuongTruyCap> LuongTruyCaps { get; set; }
        public virtual DbSet<NguoiBan> NguoiBans { get; set; }
        public virtual DbSet<NguoiMua> NguoiMuas { get; set; }
        public virtual DbSet<PhieuThanhToan> PhieuThanhToans { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheTich> TheTiches { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .Property(e => e.thestart)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.theend)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.discount)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.Ma_Coupon)
                .IsUnicode(false);

            modelBuilder.Entity<GHN_Ship>()
                .Property(e => e.return_phone)
                .IsUnicode(false);

            modelBuilder.Entity<GHN_Ship>()
                .Property(e => e.to_phone)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.mahd)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.maghn)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.HoaDonCTs)
                .WithOptional(e => e.HoaDon)
                .WillCascadeOnDelete();

            modelBuilder.Entity<LoaiSanPham>()
                .Property(e => e.hinhanh)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.LoaiSanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NguoiBan>()
                .Property(e => e.taikhoanng)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiMua>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.NguoiMua)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.barcode)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhanh1)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhanh2)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhanh3)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhanh4)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.TheTiches)
                .WithOptional(e => e.SanPham)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Size>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.password_old)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.NguoiBans)
                .WithOptional(e => e.TaiKhoan)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.NguoiMuas)
                .WithOptional(e => e.TaiKhoan)
                .WillCascadeOnDelete();
        }
    }
}
