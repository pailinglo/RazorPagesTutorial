using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorPagesTutorial.Models;

namespace RazorPagesTutorial.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee AddEmployee(Employee newEmployee)
        {
            context.Add(newEmployee);
            context.SaveChanges();
            return newEmployee;
        }

        public Employee DeleteEmployee(int id)
        {
            Employee employee = context.Employees.Find(id);
            if(employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }

            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = context.Employees;
            if (dept.HasValue)
            {
                query = query.Where(e => e.Department == dept.Value);
            }

            return query.GroupBy(e => e.Department)
                            .Select(g => new DeptHeadCount()
                            {
                                Department = g.Key.Value,
                                Count = g.Count()
                            }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            //return context.Employees.FirstOrDefault(e => e.Id == id);
            return context.Employees.Find(id);
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Employees;
            }

            return context.Employees.Where(e => e.Name.Contains(searchTerm) || e.Email.Contains(searchTerm));
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var employee = context.Employees.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedEmployee;
        }
    }
}
