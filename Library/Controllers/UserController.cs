using System;
using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase //inheritance of class ControlleBase
    {
        private readonly IUserRepository _iUserRepository;

        public UserController(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;
        }

        /// <summary>
        /// Method a control for access to the listing users
        /// </summary>
        /// <returns>A list of the users registered</returns>
        /// <exception cref="Exception">Error Message</exception>
        [HttpGet]
        public IActionResult Listing()
        {
            try
            {
                return Ok(_iUserRepository.Listing());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method a control for access to the user register
        /// </summary>
        /// <param name="user">User Object </param>
        /// <returns>Status Code Created</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _iUserRepository.Create(user);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Method a control for access to the search a user for id
        /// </summary>
        /// <param name="UserId">user finding id</param>
        /// <returns>Book found</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpGet("{Id}")]
        public IActionResult FindById(int UserId)
        {
            try
            {
                User userFinding = _iUserRepository.FindById(UserId);
                if (userFinding == null)
                {
                    return StatusCode(204);
                }
                return Ok(userFinding);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method a control for access to the update a user
        /// </summary>
        /// <param name="user"> User name</param>
        /// <param name="UserId"> User finding ID</param>
        /// <returns>User Updated</returns>
        /// <exception cref="Exception">Error message</exception>
        [HttpPut("{Id}")]
        public IActionResult Update(User user, int UserId)
        {
            try
            {
                _iUserRepository.Update(user, UserId);
                return Ok("User update successfully!!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Method a control for access to the delete a user
        /// </summary>
        /// <param name="UserId">user deleted id</param>
        /// <exception cref="Exception">Error message</exception>
        [HttpDelete("{Id}")]
        public IActionResult Delete(int UserId)
        {
            try
            {
                _iUserRepository.Delete(UserId);
                return Ok("Deleted User ! !");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}

