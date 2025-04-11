using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Db;
public class DbCommunityAgent
{
    public const string TableName = "Agents";
    public Guid Id {  get; set; }
    public Guid AgentId { get; set; }
    public Guid CommunityId { get; set; }
    public DbCommunity Community { get; set; }
    public DbCommunityAgent()
    {
        Community = new DbCommunity();
    }
}

public class DbCommunityAgentConfiguration : IEntityTypeConfiguration<DbCommunityAgent>
{
    public void Configure(EntityTypeBuilder<DbCommunityAgent> builder)
    {
        builder
          .ToTable(DbCommunityAgent.TableName);

        builder
          .HasKey(n => n.Id);

        builder
            .HasOne(a => a.Community)
            .WithMany(c => c.Agents);

    }
}