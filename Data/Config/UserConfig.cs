using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RUM.Models;

namespace RUM.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder
            .Property(u => u.Fname)
            .HasMaxLength(50)
            .IsRequired();

              builder
            .Property(u => u.Password)
            .HasMaxLength(20)
            .IsRequired();

            builder
            .Property(u => u.Lname)
            .HasMaxLength(50)
            .IsRequired();

            builder
            .Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();

            builder.HasIndex(u => u.Email).IsUnique(true);

            builder.HasMany(u => u.Messages)
         .WithOne(m => m.User)
         .HasForeignKey(m => m.UserId)
         .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
