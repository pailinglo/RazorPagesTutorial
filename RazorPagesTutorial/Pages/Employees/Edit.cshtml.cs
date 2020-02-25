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

        //[BindProperty]
        //If using this, we don't have to pass Employee parameter into OnPost method.
        //We use this approach if we need to access the posted form values outside of the OnPost() handler method.
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

        //the model binding will return the Employee object automatically.
        public IActionResult OnPost(Employee employee)
        {
            //why in the tutorial, we don't check this at all?
            //if (ModelState.IsValid)
            //{
                this.employeeRepositoy.UpdateEmployee(employee);
            //}

            return RedirectToPage("Index");

        }

    }
}