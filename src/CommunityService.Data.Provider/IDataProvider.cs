using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.Core.Attributes;
using UniversityHelper.Core.EFSupport.Provider;
using UniversityHelper.Core.Enums;
using UniversityHelper.CommunityService.Models.Db;

namespace UniversityHelper.CommunityService.Data.Provider;

[AutoInject(InjectType.Scoped)]
public interface IDataProvider : IBaseDataProvider
{
  DbSet<DbCommunity> Communities { get; set; }
  DbSet<DbCommunityAgent> Agents { get; set; }
  DbSet<DbCommunityHidden> HiddenCommunities { get; set; }
  DbSet<DbNews> News { get; set; }
  DbSet<DbNewsPhoto> NewsPhotos { get; set; }
  DbSet<DbParticipating> Participating { get; set; }

}
