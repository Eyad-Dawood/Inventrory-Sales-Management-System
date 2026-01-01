using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Validation.Exceptions
{
    public class MessageException : Exception
    {
        private readonly string _message;

        public MessageException (string message) : base (message)
        {
            _message = message;
        }
    }
}
