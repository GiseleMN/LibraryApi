using System;
using Library.Models;

namespace Library.Interfaces
{
    /// <summary>
    /// User Interface
    /// </summary>
	public interface IUserRepository
	{
        //Methods that will be implemented
        List<User> Listing();

        void Create(User user);

        User FindById(int UserId);

        void Update(User user, int UserId);

        void Delete(int UserId);

        User Login(string email, string password);
    }
}

