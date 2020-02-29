using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTutorial.Models;
using RazorPagesTutorial.Services;

namespace RazorPagesTutorial.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IEmployeeRepository employeeRepository, 
                         IWebHostEnvironment webHostEnvironment)
        {
            this.employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        //If using this, we don't have to pass Employee parameter into OnPost method.
        //We use this approach if we need to access the posted form values outside of the OnPost() handler method.
        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }
        
        [BindProperty]
        public IFormFile Photo { get; set; }

        //we are reusing Edit page to add a new employee, so we have to make id as nullable.
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = this.employeeRepository.GetEmployee(id.Value);
            }
            else
            {
                Employee = new Employee();
            }

            if(Employee == null)
            {
                return RedirectToPage("/NotFound"); 
            }


            return Page();
        }

        //the model binding will return the Employee object automatically.
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                //if user upload a file
                if(Photo != null)
                {
                    //delete the existing photo of the employee first
                    if(Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", Employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object
                    Employee.PhotoPath = ProcessUploadedFile();
                }
                
                if(Employee.Id > 0)
                {
                    this.employeeRepository.UpdateEmployee(Employee);
                }
                else
                {
                    this.employeeRepository.AddEmployee(Employee);
                }

                return RedirectToPage("Index");
            }

            //however the page is expecting an Employee object, we can annotate Employee [BindProperty]
            //Employee = employee;
            return Page();
        }

        //we still need to pass id or the Employee information will be missing
        public IActionResult OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notifications";
            }
            else
            {
                Message = "You have turned off email notifications";
            }

            //pass data to the subsequent page.
            TempData["message"] = Message;

            //redirect to Details page with object:
            return RedirectToPage("Details", new { id = id});

        }

        //This method helps to generate an unique file name for the uploaded file
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                //use webHostEnvironment to get the root folder wwwrroot
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                
                //append GUID to the uploaded file name to make it unique and avoid users upload files with same file name
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Photo.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

    }
}