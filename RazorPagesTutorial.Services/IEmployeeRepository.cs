﻿using RazorPagesTutorial.Models;
using System;
using System.Collections.Generic;

namespace RazorPagesTutorial.Services
{
    //This interface abstraction allows us to use dependency injection.
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string searchTerm);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);

        Employee UpdateEmployee(Employee updatedEmployee);
        Employee AddEmployee(Employee newEmployee);

        Employee DeleteEmployee(int id);
        IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept);
    }
}
