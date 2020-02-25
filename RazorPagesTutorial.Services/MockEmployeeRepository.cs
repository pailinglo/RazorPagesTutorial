using System;
using System.Collections.Generic;
using System.Text;
using RazorPagesTutorial.Models;
using System.Linq;

namespace RazorPagesTutorial.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = Dept.HR,
                    Email = "mary@pragimtech.com", PhotoPath="mary.png" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT,
                    Email = "john@pragimtech.com", PhotoPath="john.png" },
                new Employee() { Id = 3, Name = "Sara", Department = Dept.IT,
                    Email = "sara@pragimtech.com", PhotoPath="sara.png" },
                new Employee() { Id = 4, Name = "David", Department = Dept.Payroll,
                    Email = "david@pragimtech.com" },
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            //needs to include System.Linq
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == updatedEmployee.Id);

            if(employee != null)
            {
                employee.Email = updatedEmployee.Email;
                employee.Name = updatedEmployee.Name;
                employee.Department = updatedEmployee.Department;
            }

            return employee;
        }
    }
}
