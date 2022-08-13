using Cinephilia.Controllers;
using Cinephilia.Data;
using Cinephilia.Models;
using Microsoft.AspNetCore.Mvc;   // to extract the view result
using Microsoft.EntityFrameworkCore;   // for the in memory db
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinephilia_Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        // When a test uses a database
        // we need to 'Mock' this data
        // use in-memory databases for testing

        private ApplicationDbContext _context;
        private UsersController _controller;
        private List<User> _users = new List<User>();

        // Arrange step
        [TestInitialize]
        public void TestInitialize()
        {
            // instantiate in-memory db context
            // similar to registering your db in startup.cs
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;
            _context = new ApplicationDbContext(options);



            // mock some data

            // list of user
            var user1 = new User { UserId = 1, FirstName = "Jinal", LastName = "Patel", Email = "jipatel123@gmail.com", Gender = "female", PhoneNumber = "7894561231" };
            var user2 = new User { UserId = 2, FirstName = "Bansi", LastName = "Bera", Email = "bansibera456@gmail.com", Gender = "female", PhoneNumber = "7412589638" };
            var user3 = new User { UserId = 3, FirstName = "Vijay", LastName = "Nihalani", Email = "vnihani678@gmail.com", Gender = "male", PhoneNumber = "7532148965" };

            // add users to mock dbs
            _context.Users.Add(user1);
            _context.Users.Add(user2);
            _context.Users.Add(user3);
            _context.SaveChanges();

            // add _users to local list
            _users.Add(user1);
            _users.Add(user2);
            _users.Add(user3);

            // instantiate the controller object with mock db context
            _controller = new UsersController(_context);
        }

        // Test 1 > making sure index loads
        [TestMethod]
        public void IndexReturnsView()
        {
            // skip arrange, TestInitialize is called everytime the test runs

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        // Test 2 > making sure index loads some data
        [TestMethod]
        public void IndexReturnsProductData()
        {
            // Act > call index and retrieve data from view
            var result = _controller.Index();

            // extract view
            var viewResult = (ViewResult)result.Result;

            // Assert
            Assert.IsNotNull(viewResult);
        }

        // Test 3 > making sure that I can get a "NotFound" result if I will try to get details from a non-existant ID
        [TestMethod]
        public void DetailsReturnsNotFoundIfThatIdDoesNotExist()
        {
            var testId = 200;
            var actionResult = _controller.Details(testId);

            // convert generic ActionResult object to the expected result
            var notFoundResult = (NotFoundResult)actionResult.Result;

            // make sure app returns 404 when searching for an invalid id
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }


        // Test 4 > making sure that I can get a "NotFound" result if I will try to get details without an id
        [TestMethod]
        public void DetailsReturnsNotFoundIfThereIsNoId()
        {
            var actionResult = _controller.Details(null);

            // convert generic ActionResult object to the expected result
            var nullResult = (NotFoundResult)actionResult.Result;

            // make sure app returns 404 when searching without an id
            Assert.AreEqual(404, nullResult.StatusCode);
        }


        // Test 5 > making sure that I can get view if I will try to get details with valid id 
        [TestMethod]
        public void DetailsLoadsViewIfThereIsValidId()
        {
            var actionResult = _controller.Details(1);

            // convert generic ActionResult object to the expected result
            var viewResult = (ViewResult)actionResult.Result;

            // make sure app loads view when there is valid id
            Assert.IsNotNull(viewResult);
        }
    }
}
