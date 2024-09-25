using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TimeRange
    {
        public int Id { get; set; } 
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
