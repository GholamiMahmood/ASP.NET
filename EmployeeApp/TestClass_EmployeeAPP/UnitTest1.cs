using EmployeeApp.Controllers;
using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace TestClass_EmployeeAPP
{
    public class Tests
    {
        private ApplicationDbContext _context;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

        }

        [Test]
        public async Task Test1_Create()
        {

            var employeeController = new EmplyeeController(_context);

            var unEmployee = new EmplyeeEntity
            {
                Id = 1,
                Identifiant = 123456,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Telephone = "123456789",
                Department = "IT"
            };


            /*  il verifie que , la function de create marche bien,
              lorsque on create il sauvgarde dan la memeoire temporaire(inmemory)
             regarde setup */
            var result = await employeeController.Create(unEmployee);
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

            // comparer DbContext avec l'objet current
            var savedEmployee = await _context.Employee.FirstOrDefaultAsync(e => e.Identifiant == unEmployee.Identifiant);
            Assert.IsNotNull(savedEmployee);


        }
    }
}