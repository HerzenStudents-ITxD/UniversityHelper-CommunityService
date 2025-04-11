using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace UniversityHelper.CommunityService.Models.Db;

public class DbCommunity
{
    public const string TableName = "Communities";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AvatarId { get; set; }
    public string Text { get; set; }

    public Guid AuthorId { get; set; }
    public Guid? PointId { get; set; }

    [IgnoreParse]
    public ICollection<DbCommunityAgent> Agents { get; set; }
    public DbCommunity()
    {
        Agents = new HashSet<DbCommunityAgent>();
    }
}

public class DbCommunityConfiguration : IEntityTypeConfiguration<DbCommunity>
{
    public void Configure(EntityTypeBuilder<DbCommunity> builder)
    {
        builder
          .ToTable(DbCommunity.TableName);

        builder
          .HasKey(n => n.Id);

        builder
            .HasMany(c => c.Agents)
            .WithOne(a => a.Community);

    }
}
