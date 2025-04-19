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
    public string Avatar { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime ModifiedAtUtc { get; set; }



    [IgnoreParse]
    public ICollection<DbCommunityAgent> Agents { get; set; }
    [IgnoreParse]
    public ICollection<DbCommunityHidden> Hidden { get; set; }
    [IgnoreParse]
    public ICollection<DbNews> News { get; set; }
    public DbCommunity()
    {
        Agents = new HashSet<DbCommunityAgent>();
        Hidden = new HashSet<DbCommunityHidden>();
        News = new HashSet<DbNews>();
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
        builder
            .HasMany(c => c.Hidden)
            .WithOne(v=>v.Community);
        builder
            .HasMany(c => c.News)
            .WithOne(n => n.Community);

    }
}
