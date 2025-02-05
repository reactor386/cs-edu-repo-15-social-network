//-
using System;

using SocialNetwork.App;
using SocialNetwork.BLL.Models;


namespace SocialNetwork.PLL.Views;

public class MainView
{
    public void Show()
    {
        Console.WriteLine("Войти в профиль (нажмите 1)");
        Console.WriteLine("Зарегистрироваться (нажмите 2)");

        switch (Console.ReadLine())
        {
            case "1":
                {
                    Program.authenticationView.Show();
                    break;
                }

            case "2":
                {
                    Program.registrationView.Show();
                    break;
                }
        }
    }
}
