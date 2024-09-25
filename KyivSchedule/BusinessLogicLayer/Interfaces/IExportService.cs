using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IExportService
    {
        Task<List<ExportGroupDto>> ExportAllSchedulesAsync();
        Task<ExportGroupDto> ExportScheduleByGroupAsync(int groupNumber);
    }
}
