/*using EmployeeApp.Views.Emplyee;*/
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class EmplyeeEntity
    {
       /* Id est primary key, quand on met[key], cela va etre change a primary key*/
       public int Id { get; set; }
       [Required]
       [RegularExpression(@"^\d{6}$", ErrorMessage = "Identifiant must be 6 digits")]
        public int Identifiant{ get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Department { get; set; }

        

    }
}
