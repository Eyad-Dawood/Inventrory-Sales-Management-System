using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Validation.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public string MainBody;

        public WrongPasswordException()
            : base("فشل تسجيل الدخول")
        {
            MainBody = ErrorMessagesManager.ErrorMessages.WrongPasswordErrorMessage();
        }
    }
}
