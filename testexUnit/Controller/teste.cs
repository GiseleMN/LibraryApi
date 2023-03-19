using System.Security.Cryptography.X509Certificates;
using Library.Controllers;
using Library.ViewModels;
using Library.Models;
using Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace testexUnit.Contrtoller;

public class LoginControllerTeste
{
    [Fact]
    public void LoginController_Retornar_UsuarioInvalido()
    {
        //Arrange
        var fakeRepository = new Mock<IUserRepository>();
        fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((User)null);

        LoginViewModel dadosLogin = new LoginViewModel();

        dadosLogin.Email = "email@email.com";
        dadosLogin.Password = "232323";

        var controller = new LoginController(fakeRepository.Object);

        //Act
        var resultado = controller.Login(dadosLogin);
        //Assert
        Assert.IsType<UnauthorizedObjectResult>(resultado);

    }

    [Fact]
    public void LoginController_ReturnsToken()
    {
        //Arrange
        User userReturns = new User();
        userReturns.Email = "myEmail@email.com";
        userReturns.Password = "1234567";
        userReturns.Type = "1";

        var fakeRepository = new Mock<IUserRepository>();
        fakeRepository.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(userReturns);

        string IssuerValidacao = "Library";

        LoginViewModel dadosLogin = new LoginViewModel();
        dadosLogin.Email = "myEmail@email.com";
        dadosLogin.Password="1234567" ;
        
           var controller = new LoginController(fakeRepository.Object);

        //Act
        OkObjectResult resultado = (OkObjectResult)controller.Login(dadosLogin);
        string token = resultado.Value.ToString().Split()[3];

        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenJWT = jwtHandler.ReadJwtToken(token);

        //Assert
        Assert.Equal(IssuerValidacao, tokenJWT.Issuer);

    }
}
