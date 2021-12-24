using Microsoft.EntityFrameworkCore;

namespace GoDaddyRecordUpdater
{
    public class GDContext : DbContext
    {
        public DbSet<DNSRecord> DNSRecords { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(System.Configuration.ConfigurationManager.
                ConnectionStrings["GoDaddyContext"].ConnectionString);
        }
    }
}
