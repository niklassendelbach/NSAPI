using Microsoft.EntityFrameworkCore;
using NSApp.Models;

namespace NSApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<MemberInterest> MemberInterests { get; set; }

    }
}
