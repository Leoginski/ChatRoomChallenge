using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleChatroom.Domain.Models;

namespace SimpleChatroom.Infra.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Message> Messages => Set<Message>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}