using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practicacrud.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practicacrud.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationConsumidor,ConsumidorRole,string>
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
                re.Property(x => x.Nombre).IsRequired().HasMaxLength(100).IsUnicode(false);
                re.Property(x => x.Apellido).IsRequired().HasMaxLength(100).IsUnicode(false);
                re.Property(x => x.Direccion).IsRequired().HasMaxLength(250).IsUnicode(false);
                re.Property(x => x.Estado).IsRequired().IsUnicode(false);
            });
        }
    }
}
