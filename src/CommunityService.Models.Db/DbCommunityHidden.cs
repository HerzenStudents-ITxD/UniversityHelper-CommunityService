using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Db;
public class DbCommunityHidden
{
    public const string TableName = "HiddenCommunities";
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
    public DbCommunity Community { get; set; }

    public DbCommunityHidden()
    {
        Community = new DbCommunity();
    }
}

public class DbCommunityVisibleConfiguration : IEntityTypeConfiguration<DbCommunityHidden>
{
    public void Configure(EntityTypeBuilder<DbCommunityHidden> builder)
    {
        builder
          .ToTable(DbCommunityHidden.TableName);

        builder
          .HasKey(n => n.Id);

        builder
            .HasOne(a => a.Community)
            .WithMany(c => c.Hidden);

    }
}
