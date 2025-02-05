//-
using System;

using SocialNetwork.DAL.Repositories;
using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.PLL.Views;


namespace SocialNetwork.App;

/// <summary>
/// создаем список вью и запускаем главный вью
/// </summary>
internal class Program
{
    static MessageService messageService;
    static UserService userService;
    public static MainView mainView;
    public static RegistrationView registrationView;
    public static AuthenticationView authenticationView;
    public static UserMenuView userMenuView;
    public static UserInfoView userInfoView;
    public static UserDataUpdateView userDataUpdateView;
    public static MessageSendingView messageSendingView;
    public static UserIncomingMessageView userIncomingMessageView;
    public static UserOutcomingMessageView userOutcomingMessageView;
    public static AddingFriendView addingFriendView;
    public static UserFriendView userFriendView;


    static void Main(string[] args)
    {
        IUserRepository userRepository = new UserRepository();
        IMessageRepository messageRepository = new MessageRepository();
        IFriendRepository friendRepository = new FriendRepository();

        messageService = new MessageService(messageRepository, userRepository);
        userService = new UserService(userRepository, friendRepository, messageService);

        mainView = new MainView();
        registrationView = new RegistrationView(userService);
        authenticationView = new AuthenticationView(userService);
        userMenuView = new UserMenuView(userService);
        userInfoView = new UserInfoView();
        userDataUpdateView = new UserDataUpdateView(userService);
        messageSendingView = new MessageSendingView(messageService, userService);
        userIncomingMessageView = new UserIncomingMessageView();
        userOutcomingMessageView = new UserOutcomingMessageView();
        addingFriendView = new AddingFriendView(userService);
        userFriendView = new UserFriendView();

        while (true)
        {
            mainView.Show();
        }
    }
}
