using gRPCGraphQLWebSockets.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace gRPCGraphQLWebSockets.Database
{
    public sealed class gRPCGraphQLWebSocketsDatabaseContext : DbContext
    {
        public gRPCGraphQLWebSocketsDatabaseContext()
        {
        }

        public gRPCGraphQLWebSocketsDatabaseContext(DbContextOptions<gRPCGraphQLWebSocketsDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=Database\\gRPCGraphQLWebSocketsDatabase.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Messages_Id")
                    .IsUnique();

                entity.Property(e => e.Text).IsRequired();
            });
        }
    }
}
