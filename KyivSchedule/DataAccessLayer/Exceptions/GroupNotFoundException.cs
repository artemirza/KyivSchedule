using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException(int groupNumber) : base($"Group number {groupNumber} was not found")
        {
        }
    }
}
