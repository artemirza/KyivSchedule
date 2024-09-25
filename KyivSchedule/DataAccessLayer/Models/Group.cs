using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Group
    {
        public int GroupId { get; set; } 
        public List<TimeRange> OutageTimes { get; set; } = new List<TimeRange>();
    }
}
