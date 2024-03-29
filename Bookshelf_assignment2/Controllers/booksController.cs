﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment1.Models;
using Bookshelf_assignment2.Models;

namespace Assignment1.Controllers
{
     
    public class booksController : Controller
    {
        //private BookshelfModel db = new BookshelfModel();

        private IBooksRepository db;

        //no parameters => IF Repo
        public booksController()
        {
            this.db = new EFBooksRepository();
        }

        //moq data => I repo
        public booksController (IBooksRepository booksRepo)
        {
            this.db = booksRepo;
        } 

        // GET: books
        public ViewResult Index()
        {
            var books = db.books.Include(b => b.author);
            return View(books.ToList());
        }

        [AllowAnonymous]
        // GET: books/Details/5
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            book book = db.books.SingleOrDefault(b => b.book_id == id);
            if (book == null)
            {
                return View("Error");
            }
            return View(book);
        }

        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.author_id = new SelectList(db.authors, "author_id", "last_name");
            return View("Create");
        }

        // POST: books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "book_id,title,author_id,year_published,summary")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Save(book);
                return RedirectToAction("Index");
            }

            ViewBag.author_id = new SelectList(db.authors, "author_id", "last_name", book.author_id);
            return View("Create", book);
        }

        // GET: books/Edit/5
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            book book = db.books.SingleOrDefault(b => b.book_id == id);
            if (book == null)
            {
                return View("Error");
            }
            ViewBag.author_id = new SelectList(db.authors, "author_id", "last_name", book.author_id);
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_id,title,author_id,year_published,summary")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Save(book);
                return RedirectToAction("Index");
            }
            ViewBag.author_id = new SelectList(db.authors, "author_id", "last_name", book.author_id);
            return View("Edit", book);
        }

        // GET: books/Delete/5
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            book book = db.books.SingleOrDefault(b => b.book_id == id);
            if (book == null)
            {
                return View("Error");
            }
            return View("Delete", book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            book book = db.books.SingleOrDefault(b => b.book_id == id);
            if (book == null)
            {
                return View("Error");
            }
            db.Delete(book);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
