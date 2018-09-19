namespace FluentApi.Ef
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model :DbContext
    {
        public Model()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ContactInfo> ContactInfos
        {
            get; set;
        }
        public virtual DbSet<Employee> Employees
        {
            get; set;
        }
        public virtual DbSet<Project> Projects
        {
            get; set;
        }
        public virtual DbSet<Team> Teams
        {
            get; set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employee>()
                .HasOptional(e => e.ContactInfo)
                .WithRequired(e => e.Employee);

            modelBuilder.Entity<Project>()
                .Property(e => e.BudgetLimet)
                .HasPrecision(18, 0);
        }
    }
}
