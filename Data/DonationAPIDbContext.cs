using DonationAPI1.Models;
using Microsoft.EntityFrameworkCore;

namespace DonationAPI1.Data
{
    public class DonationAPIDbContext : DbContext
    {
        public DonationAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Donation> Donations { get; set; }
    }
}
