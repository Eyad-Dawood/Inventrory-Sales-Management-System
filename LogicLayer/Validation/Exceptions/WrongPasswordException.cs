using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Validation.Exceptions
{
    public class WrongPasswordException : Exception
    {
        string _message;

        public WrongPasswordException()
            : base("فشل تسجيل الدخول")
        {
            _message = ErrorMessagesManager.ErrorMessages.WrongPasswordErrorMessage();
        }
    }
}
