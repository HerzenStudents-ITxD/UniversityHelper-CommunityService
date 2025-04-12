using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Db;
public class DbNewsPhoto
{
    public const string TableName = "NewsPhoto";
    public Guid Id { get; set; }
    public string Photo { get; set; }
    public Guid NewsId { get; set; }
    public DbNews News { get; set; }
    public DbNewsPhoto()
    {
        News = new DbNews();
    }
}

public class DbNewsPhotoConfiguration : IEntityTypeConfiguration<DbNewsPhoto>
{
    public void Configure(EntityTypeBuilder<DbNewsPhoto> builder)
    {
        builder
          .ToTable(DbNewsPhoto.TableName);

        builder
          .HasKey(n => n.Id);

        builder
            .HasOne(n => n.News)
            .WithMany(p => p.Photos);


    }
}
