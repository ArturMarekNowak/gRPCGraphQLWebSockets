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