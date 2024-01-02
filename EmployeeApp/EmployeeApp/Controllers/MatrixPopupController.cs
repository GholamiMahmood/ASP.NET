// MatrixPopupController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using EmployeeApp.Models;
using EmployeeApp.Data;


namespace EmployeeApp.Controllers
{
    public class MatrixPopupController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MatrixPopupController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult DisplayList(int identifiant)
        {
            try
            {
                // Fetch the list of salaries based on the matching identifiant
                var salaryList = dbContext.Salary.Where(s => s.Identifiant == identifiant).ToList();
                return View("~/Views/Emplyee/MatrixPopup.cshtml", salaryList);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.ToString());

                // Return a generic error view or message
                return View("Error");
            }
        }



    }
}
