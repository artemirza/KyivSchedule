using DataAccessLayer.Data;
using DataAccessLayer.Exceptions;
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
    public class PlannerRepository : IPlannerRepository
    {
        private readonly AppDbContext _dbContext;

        public PlannerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveOrUpdateGroupsAsync(List<Group> groups)
        {
            foreach (var group in groups)
            {
                var existingGroup = await GetGroupByNumberAsync(group.GroupId);

                if (existingGroup != null)
                {
                    UpdateTimeRange(existingGroup, group.OutageTimes);
                }
                else
                {
                    await _dbContext.Groups.AddAsync(group);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOutageTimesAsync(int groupId, List<TimeRange> newOutageTimes)
        {
            var group = await GetGroupByNumberAsync(groupId);

            if (group == null)
            {
                throw new GroupNotFoundException(groupId);
            }

            UpdateTimeRange(group, newOutageTimes);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Group?> GetGroupByNumberAsync(int groupId)
        {
            return await _dbContext.Groups
                .Include(g => g.OutageTimes)
                .FirstOrDefaultAsync(g => g.GroupId == groupId);
        }

        public async Task DeleteGroupAsync(Group group)
        {
            _dbContext.Groups.Remove(group);
            await _dbContext.SaveChangesAsync();
        }

        private void UpdateTimeRange(Group existingGroup, List<TimeRange> outageTimes)
        {
            _dbContext.TimeRanges.RemoveRange(existingGroup.OutageTimes);
            existingGroup.OutageTimes = outageTimes; 
            _dbContext.Groups.Update(existingGroup);
        }
    }
}
