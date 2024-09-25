using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IPlannerRepository
    {
        Task SaveOrUpdateGroupsAsync(List<Group> groups);
        Task UpdateOutageTimesAsync(int groupId, List<TimeRange> newOutageTimes);
        Task<Group?> GetGroupByNumberAsync(int groupId);
        Task DeleteGroupAsync(Group group);
    }
}
