using Microsoft.EntityFrameworkCore;

namespace Tdp_241230817.Models;

public partial class QLHangHoaContext : DbContext
{
    public QLHangHoaContext() { }

    public QLHangHoaContext(DbContextOptions<QLHangHoaContext> options) : base(options) { }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    public virtual DbSet<LoaiHang> LoaiHangs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHang).HasName("PK_HangHoa");
            entity.ToTable("HangHoa");

            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.Gia).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TenHang).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HangHoa_LoaiHang");
        });

        modelBuilder.Entity<LoaiHang>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK_LoaiHang");
            entity.ToTable("LoaiHang");

            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });
    }
}
