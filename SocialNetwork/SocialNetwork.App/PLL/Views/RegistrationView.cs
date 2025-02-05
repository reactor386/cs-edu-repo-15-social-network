//-
using System;

using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;


namespace SocialNetwork.PLL.Views;

public class RegistrationView
{
    UserService userService;
    public RegistrationView(UserService userService)
    {
        this.userService = userService;
    }

    public void Show()
    {
        var userRegistrationData = new UserRegistrationData();

        Console.WriteLine("Для создания нового профиля введите ваше имя:");
        userRegistrationData.FirstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Ваша фамилия:");
        userRegistrationData.LastName = Console.ReadLine() ?? string.Empty;

        Console.Write("Пароль:");
        userRegistrationData.Password = Console.ReadLine() ?? string.Empty;

        Console.Write("Почтовый адрес:");
        userRegistrationData.Email = Console.ReadLine() ?? string.Empty;

        try
        {
            this.userService.Register(userRegistrationData);

            SuccessMessage.Show("Ваш профиль успешно создан."
                + " Теперь Вы можете войти в систему под своими учетными данными.");
        }

        catch (ArgumentNullException)
        {
            AlertMessage.Show("Введите корректное значение.");
        }

        catch (Exception)
        {
            AlertMessage.Show("Произошла ошибка при регистрации.");
        }
    }
}
