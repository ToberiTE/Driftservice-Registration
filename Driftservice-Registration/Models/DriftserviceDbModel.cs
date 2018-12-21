namespace Driftservice_Registration.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DriftserviceDbModel : DbContext
    {
        public DriftserviceDbModel()
            : base("name=DriftserviceDbModel")
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasMany(e => e.ServiceTypes)
                .WithMany(e => e.Contacts)
                .Map(m => m.ToTable("ContactServiceTypes").MapLeftKey("ContactID").MapRightKey("ServiceTypeID"));
        }
    }
}
