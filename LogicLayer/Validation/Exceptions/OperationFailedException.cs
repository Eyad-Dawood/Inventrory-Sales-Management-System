using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Validation.Exceptions
{

    public class OperationFailedException : Exception
    {
        public string MainBody;

        public OperationFailedException(string message)
            : base("فشلت العملية") 
        {
            MainBody = message;
        }

        public OperationFailedException()
            : base("فشلت العملية")
        {
            MainBody = Validation.ErrorMessagesManager.ErrorMessages.OperationFailedErrorMessage();
        }
    }
}
