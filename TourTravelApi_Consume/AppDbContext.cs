using Microsoft.EntityFrameworkCore;
using TourTravelApi_Consume.Models;

namespace TourTravelApi_Consume
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<ContactUsModel> ContactUs { get; set; }
    }
}
