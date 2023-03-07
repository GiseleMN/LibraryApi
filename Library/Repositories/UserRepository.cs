using System;
using Library.Contexts;
using Library.Interfaces;
using Library.Models;

namespace Library.Repositories
{
    /// <summary>
    /// ---Class inheriting the IUserRepository(User Interface)
    /// This repository for User Class. This Class when access, have a communication for the class context, and a class UserController , where get a request , and have a access to database  has storage information through context.
    /// </summary>
	public class UserRepository : IUserRepository
	{
        //creating a private variable, for a storage datas
        private readonly LibraryContext _libraryContext;

        //Dependency injection
        public UserRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext; //storage datas of the interface in variable 
        }

        /// <summary>
        /// Method for listing users registered
        /// </summary>
        /// <returns>Users listing</returns>
        public List<User> Listing()
        {
            return _libraryContext.Users.ToList();
        }

        /// <summary>
        /// Method for create a user register
        /// </summary>
        /// <param name="user"> User object</param>
        public void Create(User user)
        {
            _libraryContext.Users.Add(user);
            _libraryContext.SaveChanges();
        }

        /// <summary>
        /// Method for search a user for id
        /// </summary>
        /// <param name="UserId">id for the finding a user</param>
        /// <returns> User found by id</returns>
        public User FindById(int UserId)
        {
            return _libraryContext.Users.Find(UserId);
        }

        /// <summary>
        /// Method for update a user
        /// </summary>
        /// <param name="user">user name</param>
        /// <param name="UserId">id for the finding a user</param>
        public void Update(User user, int UserId)
        {
            User userFinding = _libraryContext.Users.Find(UserId);
            if (userFinding != null)
            {
                userFinding.Email = user.Email;
                userFinding.Password = user.Password;
                userFinding.Type = user.Type;

                _libraryContext.Users.Update(userFinding);
                _libraryContext.SaveChanges();

            }
        }

        /// <summary>
        /// Method for delete a user
        /// </summary>
        /// <param name="UserId">id for the user a delete</param>
        public void Delete(int UserId)
        {
            User userFinding = _libraryContext.Users.Find(UserId);
            _libraryContext.Users.Remove(userFinding);
            _libraryContext.SaveChanges();
        }

        /// <summary>
        /// Method for login
        /// </summary>
        /// <param name="email">verification a e-mial</param>
        /// <param name="password">verification a password</param>
        /// <returns></returns>
        public User Login(string email, string password)
        {
            return _libraryContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}

