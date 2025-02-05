//-
// using NUnit.Framework;
// using NUnit.Framework.Legacy;
// using Moq;
// using AutoFixture;
using System;

using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Entities;


namespace SocialNetwork.Tests;

[TestFixture]
public class UserServiceIntegrationTests
{
    /// <summary>
    /// интеграционный тест метода записи в базу 
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="password"></param>
    /// <param name="email"></param>
    [Test]
    [TestCase("TestFirstName", "TestLastName", "TestPassword", "TestMail@mail.ru")]
    public void Register_MustAddUserToBase(
        string firstName, string lastName, string password, string email)
    {
        UserRegistrationData userRegistrationData = new();

        userRegistrationData.FirstName = firstName;
        userRegistrationData.LastName = lastName;
        userRegistrationData.Password = password;
        userRegistrationData.Email = email;

        IUserRepository userRepository = new UserRepository();
        IMessageRepository messageRepository = new MessageRepository();
        IFriendRepository friendRepository = new FriendRepository();

        MessageService messageService = new MessageService(messageRepository, userRepository);
        UserService userService = new UserService(userRepository, friendRepository, messageService);

        User? user;

        user = null;
        try { user = userService.FindByEmail(email); } catch { }
        Assert.That(user, Is.Null);

        userService.Register(userRegistrationData);

        user = null;
        try { user = userService.FindByEmail(email); } catch { }
        Assert.That(user, Is.Not.Null);

        int id = user.Id;
        userService.DeleteById(id);

        user = null;
        try { user = userService.FindByEmail(email); } catch { }
        Assert.That(user, Is.Null);
    }
}
