using System;
using System.Threading;
using System.Threading.Tasks;
using GrpcGraphQlWebSockets.SharedModel;
using GrpcGraphQlWebSockets.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

#nullable disable

namespace GrpcGraphQlWebSockets.Database
{
    public sealed class GrpcGraphQlWebSocketsDatabaseContext : DbContext
    {
        private readonly IServiceProvider _provider;

        public GrpcGraphQlWebSocketsDatabaseContext()
        {
        }

        public GrpcGraphQlWebSocketsDatabaseContext(DbContextOptions<GrpcGraphQlWebSocketsDatabaseContext> options,
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