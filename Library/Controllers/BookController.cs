using System;
using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    [Authorize]
    public class BookController : ControllerBase  // inheritance of the class ControllerBase
    {
        private readonly IBookRepository _iBookRepository; //  creating a private variable for storage datas 

        public BookController(IBookRepository iBookRepository) // Dependency Injectable
        {
            _iBookRepository = iBookRepository; // storage datas
        }

        /// <summary>
        /// Method a control for the access a listing books
        /// </summary>
        /// <returns>A list of the books registered</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpGet]
        public IActionResult Listing()
        {
            try
            {
                return Ok(_iBookRepository.Listing());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// method a control for access a book register
        /// </summary>
        /// <param name="book">Object Book</param>
        /// <returns>Status Code Created</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpPost]
        public IActionResult Create(Book book)
        {
            try
            {
                _iBookRepository.Create(book);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method a control for access to the search a book dor id
        /// </summary>
        /// <param name="BookId">book finding id</param>
        /// <returns>Book found</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpGet("{Id}")]
        public IActionResult FindById(int BookId)
        {
            try
            {
                Book bookFinding = _iBookRepository.FindById(BookId);
                if (bookFinding == null)
                {
                    return StatusCode(204);
                }
                return Ok(bookFinding);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method a control for access to the update a book
        /// </summary>
        /// <param name="book">Book name</param>
        /// <param name="BookId"> Book finding ID </param>
        /// <returns>Book Updated</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpPut("{Id}")]
        public IActionResult Update(Book book, int BookId)
        {
            try
            {
                _iBookRepository.Update(book, BookId);
                return Ok("Book Updated successfully!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method a control for access to the delete a book
        /// </summary>
        /// <param name="BookId">book deleted id</param>
        /// <exception cref="Exception">Error message</exception>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int BookId)
        {
            try
            {
                _iBookRepository.Delete(BookId);
                return Ok("Deleted Book!! ");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

