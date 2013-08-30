using System.Data.Entity;

namespace TableSplitting
{
    public class Context : DbContext
    {
        public IDbSet<Account> Account { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Itinerary> Itineraries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Address>().ToTable("Users");
            modelBuilder.Entity<User>().HasRequired(u => u.Address).WithRequiredPrincipal(a => a.User).WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
