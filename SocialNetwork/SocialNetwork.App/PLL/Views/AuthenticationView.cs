//-
using System;

using SocialNetwork.App;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;


namespace SocialNetwork.PLL.Views;

public class AuthenticationView
{
    UserService userService;
    public AuthenticationView(UserService userService)
    {
        this.userService = userService;
    }

    public void Show()
    {
        var authenticationData = new UserAuthenticationData();

        Console.WriteLine("Введите почтовый адрес:");
        authenticationData.Email = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Введите пароль:");
        authenticationData.Password = Console.ReadLine() ?? string.Empty;

        try
        {
            var user = this.userService.Authenticate(authenticationData);

            SuccessMessage.Show("Вы успешно вошли в социальную сеть!");
            SuccessMessage.Show("Добро пожаловать " + user.FirstName);

            Program.userMenuView.Show(user);
        }

        catch (WrongPasswordException)
        {
            AlertMessage.Show("Пароль не корректный!");
        }

        catch (UserNotFoundException)
        {
            AlertMessage.Show("Пользователь не найден!");
        }

    }
}
