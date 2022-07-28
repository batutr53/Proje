using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Proje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
        //    builder.HasMany(a=>a.UserRoles).WithOne(a=>a.Role).HasForeignKey(a=>a.RoleId);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
