using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.BrokerSupport.Attributes.ParseEntity;

namespace UniversityHelper.CommunityService.Models.Db;

public class DbNews
{
    public const string TableName = "News";

    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? PointId { get; set; }
    public Guid CommunityId { get; set; }
    public Guid CreatedBy { get; set; }      
    public DateTime CreatedAtUtc { get; set; }
    public Guid ModifiedBy { get; set; }     
    public DateTime ModifiedAtUtc { get; set; }

    [IgnoreParse]
    public ICollection<DbParticipating> Participatings { get; set; }
    [IgnoreParse]
    public ICollection<DbNewsPhoto> Photos { get; set; }
    public DbCommunity Community { get; set; }

    public DbNews()
    {
        Participatings = new HashSet<DbParticipating>();
        Photos = new HashSet<DbNewsPhoto>();
    }
}

public class DbNewsConfiguration : IEntityTypeConfiguration<DbNews>
{
    public void Configure(EntityTypeBuilder<DbNews> builder)
    {
        builder
          .ToTable(DbNews.TableName);

        builder
          .HasKey(n => n.Id);

        builder
            .HasMany(n => n.Participatings)
            .WithOne(p => p.News);
        builder
            .HasMany(n => n.Photos)
            .WithOne(p => p.News);
        builder
            .HasOne(n => n.Community)
            .WithMany(c => c.News);
    }
}
