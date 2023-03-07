using System;
using Library.Models;

namespace Library.Interfaces
{
    /// <summary>
    /// Book Interface
    /// </summary>
	public interface IBookRepository
	{
        //Methods that will be implemented
        List<Book> Listing();

        void Create(Book book);

        Book FindById(int BookId);

        void Update(Book book, int BookId);

        void Delete(int BookId);
    }
}

