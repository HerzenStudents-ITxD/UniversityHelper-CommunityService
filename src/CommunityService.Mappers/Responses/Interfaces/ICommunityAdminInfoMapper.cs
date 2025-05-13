using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityHelper.CommunityService.Models.Db;
using UniversityHelper.CommunityService.Models.Dto.Models;
using UniversityHelper.Core.Attributes;

namespace UniversityHelper.CommunityService.Mappers.Responses.Interfaces;
[AutoInject]
public interface ICommunityAdminInfoMapper
{
    CommunityAdminInfo Map(DbCommunity community);
}
