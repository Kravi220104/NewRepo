//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using InsurancePolicyManagementSystems.Repository.Models;

//namespace InsurancePolicyManagementSystems.Repository.Data
//{
//    public class AppDbContext : IdentityDbContext<Customer>
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options)
//            : base(options) { }

//        //public DbSet<Customer> User { get; set; }


//        public DbSet<Customer> Customer { get; set; }

//        public DbSet<CreatePolicies> CreatePolicies { get; set; }
//        public DbSet<RegisterForPolicies> RegisterForPolicies { get; set; }
//        public DbSet<GroupMember> GroupMembers { get; set; }
//        public DbSet<Claim> Claims { get; set; }

//    }
//}
// File: InsurancePolicyManagementSystems.Repository.Data/AppDbContext.cs (FINAL FIX)

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InsurancePolicyManagementSystems.Repository.Models;

namespace InsurancePolicyManagementSystems.Repository.Data
{
    public class AppDbContext : IdentityDbContext<Customer>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSet Declarations (keeping them as you had them)
        public DbSet<CreatePolicies> CreatePolicies { get; set; }
        public DbSet<RegisterForPolicies> RegisterForPolicies { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Claim> Claims { get; set; }

        // File: InsurancePolicyManagementSystems.Repository.Data/AppDbContext.cs (ULTIMATE FIX)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Sets up Identity tables (AspNetUsers, AspNetRoles, etc.)
            base.OnModelCreating(builder);

            // ==========================================================
            // 1. DECIMAL PRECISION FIXES (To clear warnings)
            // ==========================================================

            // For CreatePolicies
            //builder.Entity<CreatePolicies>().Property(p => p.SumAssurance).HasColumnType("decimal(18, 2)");
            //builder.Entity<CreatePolicies>().Property(p => p.AmountToBePaid).HasColumnType("decimal(18, 2)");

            // For Claim
            builder.Entity<Claim>().Property(c => c.RequestedAmount).HasColumnType("decimal(18, 2)");
            builder.Entity<Claim>().Property(c => c.ApprovedAmount).HasColumnType("decimal(18, 2)");

            // ==========================================================
            // 2. MULTIPLE CASCADE PATH FIXES (SQL Error 1785)
            // ==========================================================

            // FIX A: RegisterForPolicies -> Customer (Breaks a major cycle path)
            builder.Entity<RegisterForPolicies>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // FIX B: Claims -> PolicyRegistration (Breaks the internal Claim cycle)
            // NOTE: We leave Claims -> Customer as CASCADE so deleting a user deletes their claims.
            builder.Entity<Claim>()
                .HasOne(c => c.PolicyRegistration)
                .WithMany()
                .HasForeignKey(c => c.PolicyRegistrationId)
                .OnDelete(DeleteBehavior.Restrict);

            // FIX C: GroupMember -> RegisterForPolicies (Breaks the GroupMember cycle path)
            builder.Entity<GroupMember>()
                .HasOne<RegisterForPolicies>()
                .WithMany(r => r.Members) // Ensure 'Members' is the collection name on RegisterForPolicies
                .HasForeignKey(gm => gm.PolicyRegistrationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


