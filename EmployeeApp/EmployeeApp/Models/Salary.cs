using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models
{
    
    public class Salary
    {
        public int SalaryId { get; set; } // Primary key
        public int Identifiant { get; set; }
        public DateTime Date { get; set; }       
        public decimal Salaire { get; set; }
     }
}
