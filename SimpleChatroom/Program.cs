using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleChatroom.Domain.Interfaces;
using SimpleChatroom.Domain.Services;
using SimpleChatroom.Infra.Consumers;
using SimpleChatroom.Infra.Context;
using SimpleChatroom.Infra.Hubs;
using SimpleChatroom.Repository;

namespace SimpleChatroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AddRabbitMQ(builder);
            AddDatabase(builder);

            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            builder.Services.AddTransient<IChatroomService, ChatroomService>();
            builder.Services.AddTransient<ICommandService, CommandService>();
            builder.Services.AddTransient<IChatroomRepository, ChatroomRepository>();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<ChatRoomHub>("/chatroom");

            app.Run();
        }

        private static void AddDatabase(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("ApplicationDbContext string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        private static void AddRabbitMQ(WebApplicationBuilder builder)
        {
            var rabbit = builder.Configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string 'RabbitMQ' not found.");
            builder.Services.AddMassTransit(c =>
            {
                c.AddConsumer<CommandResultConsumer>();

                c.UsingRabbitMq((context, config) =>
                {
                    config.ReceiveEndpoint("simplechatroom-result", queue =>
                    {
                        queue.ConfigureConsumer<CommandResultConsumer>(context);
                    });

                    config.Host(rabbit);
                });
            });
        }
    }
}