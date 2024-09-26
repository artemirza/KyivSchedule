using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ExportRepository : IExportRepository
    {
        private readonly AppDbContext _dbContext;

        public ExportRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Group>> GetAllGroupsAsync(int page)
        {
            var pageSize = 3f;
            var totalCount = _dbContext.Groups.Count();
            var totalPages = Math.Ceiling(totalCount / pageSize);

            if (page < 1)
            {
                page = 1;
            }

            return await _dbContext.Groups
                .Skip((page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .Include(g => g.OutageTimes)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Group?> GetGroupByNumberAsync(int groupNumber)
        {
            return await _dbContext.Groups
                .Include(g => g.OutageTimes)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GroupId == groupNumber);
        }
    }
}

