using DataAccessLayer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstractions;

namespace DataAccessLayer.Entities 
{
    [Display(Name = "العميل")]
    public class Customer : IValidatable
    {
        [Key]
        [Display(Name ="معرف العميل")]
        public int CustomerId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name ="الرصيد")]
        public decimal Balance { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        [Display(Name = "معلومات الشخص")]
        public Person Person { get; set; } 

        [Display(Name ="النشاط")]
        public bool IsActive { get; set; }

        public bool Validate(List<ValidationError>errors)
        {
            //Validate Person
            if(Person==null)
            {
                errors.Add(new ValidationError
                {
                    ObjectType = typeof(Customer),
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
