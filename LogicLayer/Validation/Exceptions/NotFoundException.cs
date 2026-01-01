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
        public string message { get; set; }

        
        public NotFoundException(Type ObjectType)
            : base("فشل العثور علي الهدف")
        {
            
            message = ErrorMessagesManager.ErrorMessages.NotFoundErrorMessage(ObjectType);
        }
    }
}
