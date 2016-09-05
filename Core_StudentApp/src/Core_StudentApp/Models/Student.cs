using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core_StudentApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[DisplayFormat(DataFormatString = "{yyyy-mm-dd}",ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
    }
}
