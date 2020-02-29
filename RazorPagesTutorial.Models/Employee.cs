using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RazorPagesTutorial.Models
{
    //make this class public
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        [Required]
        public string Email { get; set; }
        public string PhotoPath { get; set; }

        // Dept => nullable, or the "require validation" won't work. e.g.         
        // the validation error won't show even if I didn't choose a department
        [Required]
        public Dept? Department { get; set; }
    }
}
