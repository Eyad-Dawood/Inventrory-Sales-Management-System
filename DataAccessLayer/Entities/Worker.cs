using DataAccessLayer.Abstractions;
using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    [Flags]
    public enum WorkersCraftsEnum {

        None = 0,
        [Display(Name ="نجار")]
        Carpenter = 1 ,
        [Display(Name = "نقاش")]
        Painter = 2 
    }

    [Display(Name="العامل")]
    public class Worker : IValidatable
    { 
        [Key]
        [Display(Name = "معرف العامل")]
        public int WorkerId { get; set; }

        [Display(Name = "الحرفة")]
        public WorkersCraftsEnum Craft { get; set; }

        [Display(Name = "النشاط")]
        public bool IsActive { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        [Display(Name = "معلومات الشخص")]
        public Person Person { get; set; }



        public bool Validate(List<ValidationError>errors)
        {
            //Validate Person
            if (Person == null)
            {
                errors.Add(new ValidationError
                {
                    ObjectType = typeof(Worker),
                    PropertyName = nameof(Person),
                    Code = ValidationErrorCode.RequiredFieldMissing
                }
                );
            }
            else
                Person.Validate(errors);

            return errors.Count == 0;
        }
    }
}
