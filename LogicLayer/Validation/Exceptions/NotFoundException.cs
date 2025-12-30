using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LogicLayer.Validation.Exceptions
{
    public class NotFoundException : Exception
    {
        string _message;

        
        public NotFoundException(Type ObjectType)
            : base("فشل العثور علي الهدف")
        {
            
            _message = ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(ObjectType);
        }
    }
}
