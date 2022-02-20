using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class Compensation
    { 
        // The primary key
        [Key] public String EmployeeId { get; set; }
        
        // Decimal for currency DateTime for date
        public Decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}