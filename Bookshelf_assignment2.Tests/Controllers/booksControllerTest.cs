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

        public booksControllerTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
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
