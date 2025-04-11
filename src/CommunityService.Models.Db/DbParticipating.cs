using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Db;
public class DbParticipating
{
    public const string TableName = "Participating";

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid NewsId { get; set; }
    public DbNews News { get; set; }
}

public class DbParticipatingConfiguration : IEntityTypeConfiguration<DbParticipating>
{
    public void Configure(EntityTypeBuilder<DbParticipating> builder)
    {
        builder
          .ToTable(DbParticipating.TableName);

        builder
          .HasKey(p => p.Id);

        builder
            .HasOne(p => p.News)
            .WithMany(n => n.Participatings);
    }
}
