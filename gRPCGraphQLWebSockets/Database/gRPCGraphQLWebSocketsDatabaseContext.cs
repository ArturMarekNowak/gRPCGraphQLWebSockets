using System;
using System.Threading;
using System.Threading.Tasks;
using gRPCGraphQLWebSockets.SharedModel;
using gRPCGraphQLWebSockets.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

#nullable disable

namespace gRPCGraphQLWebSockets.Database
{
    public sealed class gRPCGraphQLWebSocketsDatabaseContext : DbContext
    {
        private readonly IServiceProvider _provider;

        public gRPCGraphQLWebSocketsDatabaseContext()
        {
        }

        public gRPCGraphQLWebSocketsDatabaseContext(DbContextOptions<gRPCGraphQLWebSocketsDatabaseContext> options,
            IServiceProvider provider)
            : base(options)
        {
            _provider = provider;
        }

        private IHubContext<SignalRMessageHub> messageHub =>
            _provider.GetRequiredService<IHubContext<SignalRMessageHub>>();

        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
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

        public override async ValueTask<EntityEntry<Message>> AddAsync<Message>(Message message,
            CancellationToken cancellationToken = new())
        {
            await messageHub.Clients.All.SendAsync("ReceiveMessage", message.ToString());

            return await base.AddAsync(message, cancellationToken);
        }
    }
}