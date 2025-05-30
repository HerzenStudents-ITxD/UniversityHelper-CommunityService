﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityHelper.CommunityService.Models.Dto.Requests.News;
public class EditNewsRequest
{
    public string Title { get; set; }
    public string Text { get; set; }
    public List<string> Images { get; set; }
    public Guid? PointId { get; set; }
}
