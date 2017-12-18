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

        public IQueryable<Book> Books { get{ return db.books; } }

        public IQueryable<author> Authors { get { return db.authors; } } 
        public void Delete(Book book)
        {
            db.books.Remove(book);
            db.SaveChanges();
        }

        public Book Save(Book book)
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
