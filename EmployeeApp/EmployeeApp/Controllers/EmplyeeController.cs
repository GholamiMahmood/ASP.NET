using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace EmployeeApp.Controllers
{
    [Authorize]
    public class EmplyeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmplyeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emplyee
        public async Task<IActionResult> Index()
        {
              return _context.Employee != null ? 
                          View(await _context.Employee.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Employee'  is null.");
        }
        public async Task<IActionResult> Graphiques ()
        {
            return _context.Employee != null ?
                        View(await _context.Employee.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Employee'  is null.");
        }

        // GET: Emplyee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var emplyeeEntity = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emplyeeEntity == null)
            {
                return NotFound();
            }

            return View(emplyeeEntity);
        }
        public IActionResult Salaire(int? personalId)
        {
            if (personalId.HasValue)
            {
                var employee = _context.Employee.FirstOrDefault(e => e.Identifiant == personalId);

                if (employee != null)
                {
                    // Employee found, pass it to the view
                    return View(employee);
                }
                else
                {
                    // Employee not found, set error message
                    ViewData["SearchMessage"] = "Employee with the specified Identifiant does not exist.";
                }
            }
            else
            {
                // Invalid or null Identifiant provided
                ViewData["SearchMessage"] = "Please enter a valid Identifiant.";
            }

            // Return to the search page or display an error view
            return View();
        }


        // GET: Emplyee/Create
        public IActionResult Create()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Identifiant,FirstName,LastName,Email,Telephone,Department")] EmplyeeEntity emplyeeEntity)
        {
            // Check if Identifiant already exists in the database
            if (_context.Employee.Any(e => e.Identifiant == emplyeeEntity.Identifiant))
            {
                ModelState.AddModelError("Identifiant", "L'identifiant existe déjà.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(emplyeeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(emplyeeEntity);
        }
        // POST: Emplyee/Edit/5
        // GET: Emplyee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var emplyeeEntity = await _context.Employee.FindAsync(id);
            if (emplyeeEntity == null)
            {
                return NotFound();
            }
            return View(emplyeeEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Identifiant,FirstName,LastName,Email,Telephone,Department")] EmplyeeEntity emplyeeEntity)
           {
               if (id != emplyeeEntity.Id)
               {
                   return NotFound();
               }

               if (ModelState.IsValid)
               {
                   try
                   {
                       _context.Update(emplyeeEntity);
                       await _context.SaveChangesAsync();
                   }
                   catch (DbUpdateConcurrencyException)
                   {
                       if (!EmplyeeEntityExists(emplyeeEntity.Id))
                       {
                           return NotFound();
                       }
                       else
                       {
                           throw;
                       }
                   }
                   return RedirectToAction(nameof(Index));
               }
               return View(emplyeeEntity);
           }

        // GET: Emplyee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var emplyeeEntity = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emplyeeEntity == null)
            {
                return NotFound();
            }

            return View(emplyeeEntity);
        }

        // POST: Emplyee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employee'  is null.");
            }
            var emplyeeEntity = await _context.Employee.FindAsync(id);
            if (emplyeeEntity != null)
            {
                _context.Employee.Remove(emplyeeEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
        [HttpPost]
        public IActionResult RegisterSalary(int identifiant, DateTime date, decimal salaire)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Handle invalid model state (e.g., display validation errors)
                    ViewData["ErrorMessage"] = "Invalid form data.";
                    return View();
                }

                // Use the injected database context to interact with the database.
                // The context is injected through the constructor.
                // Ensure that your dependency injection is set up correctly.

                // Save the data to the database.
                var newSalary = new Salary
                {
                    Identifiant = identifiant,
                    Salaire = salaire,
                    Date = date
                    // Add other properties as needed
                };

                _context.Salary.Add(newSalary);
                _context.SaveChanges();

                // Optionally, you can return a success message or redirect to another page.
                ViewData["RegisterMessage"] = "Salary registered successfully.";

                return View("Salaire");
                //return PartialView("Salaire");
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, display an error message, etc.)
                ViewData["ErrorMessage"] = "An error occurred during salary registration.";
                return View("Salaire");
            }
        }

        private bool EmplyeeEntityExists(int id)
        {
          return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
