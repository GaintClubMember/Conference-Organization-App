﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Conference_Organization_App.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_Entities : DbContext
    {
        public DB_Entities()
            : base("name=DB_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActivitiesMain> ActivitiesMain { get; set; }
        public virtual DbSet<ActivityNames> ActivityNames { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<EventNames> EventNames { get; set; }
        public virtual DbSet<EventsMain> EventsMain { get; set; }
        public virtual DbSet<Genders> Genders { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserEventDirections> UserEventDirections { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersDirections> UsersDirections { get; set; }
    }
}
