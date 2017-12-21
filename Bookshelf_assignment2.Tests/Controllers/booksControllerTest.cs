using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//additioanl references
using System.Web.Mvc;
using Bookshelf_assignment2.Controllers;
using Assignment1.Controllers;
using Moq;
using Bookshelf_assignment2.Models;
using Assignment1.Models;
using System.Linq;

namespace Bookshelf_assignment2.Tests.Controllers
{
    /// <summary>
    /// Summary description for booksControllerTest
    /// </summary>
    [TestClass]
    public class booksControllerTest
    {
        booksController controller;
        Mock<IBooksRepository> mock;
        List<book> books;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            //pass mock data to 2nd controller
            mock = new Mock<IBooksRepository>();

            //create mock data
            books = new List<book>
            {
                new book {book_id = 1, title = "Book1", author_id = 1, year_published = 1998, summary = "Book1"},
                new book {book_id = 2, title = "Book2", author_id = 1, year_published = 1999, summary = "Book2"},
                new book {book_id = 3, title = "Book3", author_id = 1, year_published = 2000, summary = "Book3"}
            };

            //populate mock data
            mock.Setup(m => m.books).Returns(books.AsQueryable());

            controller = new booksController(mock.Object);
            
        }

        [TestMethod]
        public void IndexViewLoads()
        {
            //Arrange

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assets
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexReturnsBooks()
        {
            var actual = (List<book>)controller.Index().Model;
            CollectionAssert.AreEqual(books, actual);
        }

        [TestMethod]
        public void DetailsValidBook()
        {
            var actual = (book)controller.Details(1).Model;
            Assert.AreEqual(books.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidBook()
        {
            var actual = (book)controller.Details(12345).Model;
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void DetailsInvalidNoId()
        {
            int? id = null;
            var actual = controller.Details(id);
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditValidBook()
        {
            var actual = (book)controller.Edit(1).Model;
            Assert.AreEqual(books.ToList()[0], actual);
        }

        [TestMethod]
        public void EditInvalidNoId()
        {
            int? id = null;
            var actual = (ViewResult)controller.Edit(id);
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditBookInvalidId()
        {
            ViewResult result = controller.Edit(555) as ViewResult;
            Assert.AreEqual("Error", result.ViewName);
        }
        
        [TestMethod]
        public void DeleteValidBook()
        {        
            var actual = (book)controller.Delete(1).Model;           
            Assert.AreEqual(books.ToList()[0], actual);
        }
        
        [TestMethod]
        public void DeleteBookInvalidId()
        {
            int id = 12345;
            ViewResult actual = (ViewResult)controller.Delete(id);
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidNoId()
        {        
            int? id = null;        
            ViewResult actual = controller.Delete(id);  
            Assert.AreEqual("Error", actual.ViewName);
        }
        
        [TestMethod]
        public void CreateViewLoads()
        {
            ViewResult actual = (ViewResult)controller.Create();
            Assert.AreEqual("Create", actual.ViewName);
            Assert.IsNotNull(actual.ViewBag.author_id);
        }
        
        [TestMethod]
        public void CreateValidBook()
        {
            book book = new book
            {
                book_id = 4,
                title = "Book4",
                author_id = 1,
                year_published = 1994,
                summary = "Book4"
            };
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(book);
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidBook()
        {
            controller.ModelState.AddModelError("key", "error message");

            book book = new book
            {
                book_id = 4,
                title = "Book4",
                author_id = 1,
                year_published = 1994,
                summary = "Book4"
            };
            ViewResult actual = (ViewResult)controller.Create(book);
            Assert.AreEqual("Create", actual.ViewName);
            Assert.IsNotNull(actual.ViewBag.author_id);
        }
        
        [TestMethod]
        public void EditValidBookPost()
        {
            book book = books.ToList()[0];
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(book);
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditInvalidBookPost()
        {
            controller.ModelState.AddModelError("key", "error message");

            book book = new book
            {
                book_id = 4,
                title = "Book4",
                author_id = 1,
                year_published = 1994,
                summary = "Book4"
            };
            ViewResult actual = (ViewResult)controller.Edit(book);
            Assert.AreEqual("Edit", actual.ViewName);
            Assert.IsNotNull(actual.ViewBag.author_id);
        }
        
        [TestMethod]
        public void DeleteConfirmedValidBook()
        {
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.DeleteConfirmed(1);          
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }
        
        [TestMethod]
        public void DeleteConfirmedBookInvalidId()
        {
            int id = 666333;
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(id);
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedBookInvalidNoId()
        {         
            int? id = null; 
            ViewResult actual = (ViewResult)controller.DeleteConfirmed(id);        
            Assert.AreEqual("Error", actual.ViewName);
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

    }
}
