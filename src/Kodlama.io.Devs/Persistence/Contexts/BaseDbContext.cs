using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public  DbSet<RefreshToken> RefreshTokens { get; set; }
        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(a =>
            {
                a.ToTable("Languages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(a =>
            {
                a.ToTable("Technologies").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.LanguageId).HasColumnName("LanguageId");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasOne(p => p.Language);
            });


            modelBuilder.Entity<Social>(a =>
            {
                a.ToTable("Socials").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.DeveloperId).HasColumnName("DeveloperId");
                a.Property(p => p.Github).HasColumnName("Github");
                a.HasOne(p => p.User);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(t => t.Id);
                a.Property(t => t.Id).HasColumnName("Id");
                a.Property(t => t.FirstName).HasColumnName("FirstName");
                a.Property(t => t.LastName).HasColumnName("LastName");
                a.Property(t => t.Email).HasColumnName("Email");
                a.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
                a.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(t => t.Status).HasColumnName("Status");
                a.Property(t => t.AuthenticatorType).HasColumnName("AuthenticatorType");
                a.HasMany(t => t.UserOperationClaims);
                a.HasMany(t => t.RefreshTokens);
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(t => t.Id);
                a.Property(t => t.Id).HasColumnName("Id");
                a.Property(t => t.UserId).HasColumnName("UserId");
                a.Property(t => t.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasOne(t => t.User);
                a.HasOne(t => t.OperationClaim);
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(t => t.Id);
                a.Property(t => t.Id).HasColumnName("Id");
                a.Property(t => t.Name).HasColumnName("Name");
            });

            Language[] languageEntitySeeds = { new(1, "C#"), new(2, "JAVA") };
            modelBuilder.Entity<Language>().HasData(languageEntitySeeds);

            Technology[] technologyEntitySeeds = { new(1, 1, "ASP.NET"), new(2, 2, "Spring") };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);

            OperationClaim[] operationClaimSeeds = { new(1, "Admin"), new(2, "User") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimSeeds);
        }
    }
}
