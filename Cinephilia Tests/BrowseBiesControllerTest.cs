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
    public class BrowseBiesControllerTest
    {
        // When a test uses a database
        // we need to 'Mock' this data
        // use in-memory databases for testing

        private ApplicationDbContext _context;
        private BrowseBiesController _controller;
        private List<BrowseBy> _browseBies = new List<BrowseBy>();

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

            // users
            var user = new User { UserId = 1, FirstName = "Jinal", LastName = "Patel", Email = "jipatel123@gmail.com", Gender = "female", PhoneNumber = "7894561234" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // entertainments
            var entertainment = new Entertainment { EntertainmentId = 1, Category = "Movies" };
            _context.Entertainments.Add(entertainment);
            _context.SaveChanges();

            // list of browse
            var browseBy1 = new BrowseBy { BrowseById = 1, Genre = "Romantic", User = user, Entertainment = entertainment };
            var browseBy2 = new BrowseBy { BrowseById = 2, Genre = "Action", User = user, Entertainment = entertainment };
            var browseBy3 = new BrowseBy { BrowseById = 3, Genre = "Comedy", User = user, Entertainment = entertainment };

            // add browsebies to mock dbs
            _context.BrowseBies.Add(browseBy1);
            _context.BrowseBies.Add(browseBy2);
            _context.BrowseBies.Add(browseBy3);
            _context.SaveChanges();

            // add _browseBies to local list
            _browseBies.Add(browseBy1);
            _browseBies.Add(browseBy2);
            _browseBies.Add(browseBy3);

            // instantiate the controller object with mock db context
            _controller = new BrowseBiesController(_context);
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
