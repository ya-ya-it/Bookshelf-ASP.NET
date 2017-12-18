using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf_assignment2.Models
{
    interface IBooksRepository
    {
        IQueryable<Book> Books { get; }

        Book Save(Book book);

    }
}
