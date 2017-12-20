using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf_assignment2.Models
{
    public interface IBooksRepository
    {
        IQueryable<book> books { get; }
        IQueryable<author> authors { get; }

        book Save(book book);

        void Delete(book book);

    }
}
