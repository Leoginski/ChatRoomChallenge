using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleChatroom.Consumer;
using SimpleChatroom.Domain.Interfaces;
using SimpleChatroom.Domain.Services;
using SimpleChatroom.Hubs;
using SimpleChatroom.Infra.Context;
using SimpleChatroom.Repository;

namespace SimpleChatroom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("ApplicationDbContext string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            builder.Services.AddSignalR();

            builder.Services.AddTransient<IChatroomService, ChatroomService>();
            builder.Services.AddTransient<ICommandService, CommandService>();
            builder.Services.AddTransient<IChatroomRepository, ChatroomRepository>();

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
    }
}