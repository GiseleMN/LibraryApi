using System;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Library.Models;
using Library.Interfaces;

namespace Library.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase//inheritance of the class ControllerBase
    {
        private readonly IUserRepository _iUserRepository;//creating a private variable for  storage datas  of the interface

        public LoginController(IUserRepository iUserRepository)//Dependency Injectable
        {
            _iUserRepository = iUserRepository; //storage datas of the interface in variable 
        }

        /// <summary>
        /// Method a control for the access to login
        /// </summary>
        /// <param name="login">user data: email and password</param>
        /// <returns>token to access</returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                //variable for storage datas a user found
                User userFinding = _iUserRepository.Login(login.Email, login.Password);

                //Case not found a user, return a error message
                if (userFinding == null)
                {
                    return Unauthorized(new { msg = "Email and/or Password is invaild !!" });
                }

                //Case found, continue for the create a token  

                // define the datas that is provider in the token - Payload
                var minhasClaims = new[]
                {
                // storage in the Claim the e-mail of the user authentic 
                new Claim(JwtRegisteredClaimNames.Email, userFinding.Email),

                // storage in the Claim the ID of the user authentic
                new Claim(JwtRegisteredClaimNames.Jti, userFinding.UserId.ToString()),

                // storage in the Claim the type of the user authentic
                new Claim(ClaimTypes.Role, userFinding.Type)
            };

                // define the key for the access to token
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("library-chave-autenticacao"));

                // define the credencials to the token - Header
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                // generated to the token
                var meuToken = new JwtSecurityToken(
                    issuer: "Library",// issuer to the token
                    audience: "Library",// destination for the token
                    claims: minhasClaims,// datas defined in the claims
                    expires: DateTime.Now.AddMinutes(60),// time for the expiration
                    signingCredentials: credenciais// credencials to the token
                    );
                // return Ok -  token
                return Ok(
                    new { token = new JwtSecurityTokenHandler().WriteToken(meuToken) }
                    );
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}

