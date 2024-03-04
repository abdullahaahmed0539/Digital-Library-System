using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Infrastructure.Utilities
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException()
        {

        }
        public AppException(string message) : base(message) { }
        public AppException(string message, Exception innerException)
          : base(message, innerException)
        {
        }
        protected AppException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
