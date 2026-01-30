using DailyUpdates.Models;
using Microsoft.EntityFrameworkCore;


namespace DailyUpdates.Data
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext ( DbContextOptions<AppDbcontext>options)
        :base (options) {}

       public DbSet<User> Users {get; set;}
       public DbSet<Report> Reports {get; set;}
    
    }
}