using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models
{
    
    public class Salary
    {
        /*c'est primary key, pourquoi? pareque Salary+Id, si j ecrit MyprogramId,
        le program ne fait pas le primary key*/ 
        public int SalaryId { get; set; }
        public int Identifiant { get; set; }
        public DateTime Date { get; set; }       
        public decimal Salaire { get; set; }
     }
}
