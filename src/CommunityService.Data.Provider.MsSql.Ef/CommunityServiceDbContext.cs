using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Data.Provider.MsSql.Ef;

public class CommunityServiceDbContext : DbContext, IDataProvider
{
    public DbSet<DbCommunity> Communities { get; set; }
    public DbSet<DbCommunityAgent> Agents { get; set; }
    public DbSet<DbCommunityHidden> HiddenCommunities { get; set; }
    public DbSet<DbNews> News { get; set; }
    public DbSet<DbNewsPhoto> NewsPhotos { get; set; }
    public DbSet<DbParticipating> Participating { get; set; }

    public CommunityServiceDbContext(DbContextOptions<CommunityServiceDbContext> options)
    : base(options)
  {
  }

  // Fluent API is written here.
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("UniversityHelper.CommunityService.Models.Db"));
  }

  public object MakeEntityDetached(object obj)
  {
    Entry(obj).State = EntityState.Detached;
    return Entry(obj).State;
  }

  async Task IBaseDataProvider.SaveAsync()
  {
    await SaveChangesAsync();
  }

  void IBaseDataProvider.Save()
  {
    SaveChanges();
  }

  public void EnsureDeleted()
  {
    Database.EnsureDeleted();
  }

  public bool IsInMemory()
  {
    return Database.IsInMemory();
  }
}
