using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Models;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepositoy;

        public EditModel(IEmployeeRepository employeeRepositoy)
        {
            this.employeeRepositoy = employeeRepositoy;
        }

        public Employee Employee { get; set; }
        public IActionResult OnGet(int id)
        {
            Employee = this.employeeRepositoy.GetEmployee(id);

            if(Employee == null)
            {
                return RedirectToPage("/NotFound"); 
            }


            return Page();
        }
    }
}