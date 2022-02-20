using challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Services
{
    // Repurposed from IEmployeeService
    public interface ICompensationService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation compensation);
        Compensation Replace(Compensation originalCompensation, Compensation newCompensation);
    }
}