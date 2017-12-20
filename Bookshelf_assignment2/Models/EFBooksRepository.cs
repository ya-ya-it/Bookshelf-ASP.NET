using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//references
using System.Web;
using System.Data.Entity;
using Assignment1.Models;

namespace Bookshelf_assignment2.Models
{
    public class EFBooksRepository : IBooksRepository
    {
        //db
        BookshelfModel db = new BookshelfModel();

        public IQueryable<book> books { get{ return db.books; } }

        public IQueryable<author> authors { get { return db.authors; } }

        public void Delete(book book)
        {
            db.books.Remove(book);
            db.SaveChanges();
        }

        public book Save(book book)
        {
            if (book.book_id == 0)
            {
                db.books.Add(book);
            }
            else
            {
                db.Entry(book).State = EntityState.Modified;
            }
            db.SaveChanges();

            return book;
        }
    }
}
