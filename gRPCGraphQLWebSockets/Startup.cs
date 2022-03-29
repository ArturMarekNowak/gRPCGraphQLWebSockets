using System;
using System.IO;
using System.Reflection;
using gRPCGraphQLWebSockets.Database;
using gRPCGraphQLWebSockets.GraphQL;
using gRPCGraphQLWebSockets.gRPC;
using gRPCGraphQLWebSockets.Rest.Services;
using gRPCGraphQLWebSockets.Rest.Services.Interfaces;
using gRPCGraphQLWebSockets.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace gRPCGraphQLWebSockets
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddGrpcHttpApi();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "gRPCGraphQLWebSockets", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddGrpcSwagger();

            services.AddDbContext<gRPCGraphQLWebSocketsDatabaseContext>();
            services.AddScoped<IRESTMessagesService, RESTMessagesService>();

            services.AddGraphQLServer()
                .AddQueryType<GraphQLMessagesQuery>()
                .AddMutationType<GraphQLMessagesMutation>()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = _environment.IsDevelopment());

            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("http://localhost:5000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "gRPCGraphQLWebSockets v1"); });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<gRPCMessagesService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response
                        .WriteAsync(
                            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
                endpoints.MapGraphQL();
                endpoints.MapHub<SignalRMessageHub>("/signalr");
            });

            app.UseCors();
        }
    }
}