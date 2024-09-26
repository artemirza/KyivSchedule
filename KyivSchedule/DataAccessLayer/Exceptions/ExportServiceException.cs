using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Exceptions
{
    public class ExportServiceException : Exception
    {
        public ExportServiceException(string message)
            : base(message)
        {
        }

        public ExportServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
