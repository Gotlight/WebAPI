﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RESTAURANTEntities2 : DbContext
    {
        public RESTAURANTEntities2()
            : base("name=RESTAURANTEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MenuCatalog> MenuCatalogs { get; set; }
        public virtual DbSet<MenuCatalogLocalization> MenuCatalogLocalizations { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<MenuItemLocalization> MenuItemLocalizations { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationPhoneNumber> OrganizationPhoneNumbers { get; set; }
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
