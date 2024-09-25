using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ExportGroupDto
    {
        public int GroupId { get; set; }
        public List<ExportTimeRangeDto> OutageTimes { get; set; } = new List<ExportTimeRangeDto>();
    }
}
