using Gateway.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gateway.Data;

public partial class HowToLearnDbContext : DbContext
{
    public HowToLearnDbContext() { }

    public HowToLearnDbContext(DbContextOptions<HowToLearnDbContext> options, IConfiguration configuration)
        : base(options)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<SectionTopic> SectionTopics { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                Configuration["Data:Database:ConnectionString"]
                );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Section>(entity =>
        {
            entity.ToTable("Section");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.ToTable("Topic");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(150);
        });


        modelBuilder.Entity<SectionTopic>(entity =>
        {
            entity.ToTable("SectionTopic");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Section).WithMany(p => p.SectionTopics)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SectionTopic_Section");

            entity.HasOne(d => d.Topic).WithMany(p => p.SectionTopics)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SectionTopic_Topic");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
