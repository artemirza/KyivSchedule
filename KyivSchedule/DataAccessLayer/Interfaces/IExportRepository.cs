﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IExportRepository
    {
        Task<List<Group>> GetAllGroupsAsync(int page);
        Task<Group?> GetGroupByNumberAsync(int groupNumber);
    }
}
