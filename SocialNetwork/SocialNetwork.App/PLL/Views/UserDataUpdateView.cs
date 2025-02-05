//-
using System;

using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;


namespace SocialNetwork.PLL.Views;

public class UserDataUpdateView
{
    UserService userService;
    public UserDataUpdateView(UserService userService)
    {
        this.userService = userService;
    }

    public void Show(User user)
    {
        Console.Write("Меня зовут:");
        user.FirstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Моя фамилия:");
        user.LastName = Console.ReadLine() ?? string.Empty;

        Console.Write("Ссылка на моё фото:");
        user.Photo = Console.ReadLine() ?? string.Empty;

        Console.Write("Мой любимый фильм:");
        user.FavoriteMovie = Console.ReadLine() ?? string.Empty;

        Console.Write("Моя любимая книга:");
        user.FavoriteBook = Console.ReadLine() ?? string.Empty;

        this.userService.Update(user);

        SuccessMessage.Show("Ваш профиль успешно обновлён!");
    }
}
