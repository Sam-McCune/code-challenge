using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;
using Newtonsoft.Json.Serialization;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }
        
        // Task 1 Reporting Structure
        // Added to get the reporting structure given an employee
        public ReportingStructure GetReportingStructureWithId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                // Get the employee
                Employee employee = GetById(id);

                // Create and return reporting structure
                return new ReportingStructure
                {
                    ReportEmployee = employee,
                    NumberOfReports = CountReports(employee)
                };
            }
            return null;
        }
    
        // Count the number of reports for the reporting structure
        private int CountReports(Employee employee)
        {
            var count = 0;
            if (employee.DirectReports == null) return count;
            foreach(var t in employee.DirectReports)
            {
                count += CountReports(t) + 1;
            }
            return count;
        }
    }
}
