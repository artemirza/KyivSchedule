using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPlannerService
    {
        Task ImportFromFileAsync(IFormFile file);
        Task UpdateOutageTimesAsync(int groupNumber, List<string> newOutageTimesStrings);
        Task<bool> IsGroupInOutageNowAsync(int groupNumber);
        Task DeleteGroupAsync(int groupNumber);
    }
}
