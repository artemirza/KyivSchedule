using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.GroupId);

            builder.HasMany(g => g.OutageTimes)
                   .WithOne(tr => tr.Group)
                   .HasForeignKey(tr => tr.GroupId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
