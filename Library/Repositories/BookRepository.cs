using System;
using Library.Contexts;
using Library.Interfaces;
using Library.Models;

namespace Library.Repositories
{
    /// <summary>
    /// ---Class inheriting the IBookRepository(Book Interface)
    /// This repository for Book Class. This Class when access, have a communication for the class context, and a class BookController , where get a request , and have a access to database  has storage information through context.
    /// </summary>
	public class BookRepository : IBookRepository
	{
        //creating a private variable, for a storage datas
        private readonly LibraryContext _libraryContext;

        //Dependency injection
        public BookRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext; //storage datas of the interface in variable 
        }

        /// <summary>
        /// Method for listing books registered
        /// </summary>
        /// <returns>Books listing</returns>
        public List<Book> Listing()
        {
            return _libraryContext.Books.ToList();
        }

        /// <summary>
        /// Method for create a book registered
        /// </summary>
        /// <param name="book">Book object</param>
        public void Create(Book book)
        {
            _libraryContext.Books.Add(book);
            _libraryContext.SaveChanges();
        }

        /// <summary>
        /// Method for search a book for id
        /// </summary>
        /// <param name="BookId">Id for the finding a book</param>
        /// <returns>book founded by id</returns>
        public Book FindById(int BookId)
        {
            return _libraryContext.Books.Find(BookId);
        }

        /// <summary>
        /// Method for update a book 
        /// </summary>
        /// <param name="book">Book name</param>
        /// <param name="BookId">id for the finding a book</param>
        public void Update(Book book, int BookId)
        {
            Book bookFinding = _libraryContext.Books.Find(BookId);

            if (bookFinding != null)
            {

                bookFinding.Title = book.Title;
                bookFinding.NumberOfPages = book.NumberOfPages;
                bookFinding.Availability = book.Availability;

                _libraryContext.Books.Update(bookFinding);
                _libraryContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method for delete a book
        /// </summary>
        /// <param name="BookId">id for the book to delete</param>
        public void Delete(int BookId)
        {
            Book bookFinding = _libraryContext.Books.Find(BookId);
            _libraryContext.Books.Remove(bookFinding);
            _libraryContext.SaveChanges();
        }
    }
}

