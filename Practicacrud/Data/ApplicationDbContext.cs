using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practicacrud.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practicacrud.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Registro> Registro { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Registro>(re =>
            {
                re.HasKey(x => x.Codigo);
                re.Property(x => x.Nombre).HasMaxLength(100).IsUnicode(false);
                re.Property(x => x.Apellido).HasMaxLength(100).IsUnicode(false);
                re.Property(x => x.Direccion).HasMaxLength(250).IsUnicode(false);
                re.Property(x => x.Estado).IsUnicode(false);
            });
        }
    }
}
