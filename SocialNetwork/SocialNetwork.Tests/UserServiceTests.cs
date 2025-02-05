//-
// using NUnit.Framework;
// using NUnit.Framework.Legacy;
using Moq;
using AutoFixture;
using System;

using SocialNetwork.BLL.Services;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Entities;


namespace SocialNetwork.Tests;

[TestFixture]
public class UserServiceTests
{
    /// <summary>
    /// тест с заданием свойств объекта параметрами
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="password"></param>
    /// <param name="email"></param>
    [Test]
    [TestCase("", "TestLastName", "TestPassword", "TestMail@mail.ru")]
    [TestCase("TestFirstName", "", "TestPassword", "TestMail@mail.ru")]
    [TestCase("TestFirstName", "TestLastName", "", "TestMail@mail.ru")]
    [TestCase("TestFirstName", "TestLastName", "TestPassword", "")]
    public void Register_MustThrowException_ParamVersion(
        string firstName, string lastName, string password, string email)
    {
        UserRegistrationData userRegistrationData = new();

        userRegistrationData.FirstName = firstName;
        userRegistrationData.LastName = lastName;
        userRegistrationData.Password = password;
        userRegistrationData.Email = email;

        Mock<IUserRepository> userRepositoryMock = new();
        Mock<IMessageRepository> messageRepositoryMock = new();
        Mock<IFriendRepository> friendRepositoryMock = new();

        UserEntity userEntity = new();
        userRepositoryMock.Setup(x => x.Create(userEntity)).Returns(1);

        MessageService messageService = new(messageRepositoryMock.Object, userRepositoryMock.Object);
        UserService userService = new(userRepositoryMock.Object, friendRepositoryMock.Object, messageService);

        Assert.Throws<ArgumentNullException>(
            () => userService.Register(userRegistrationData));
    }


    /// <summary>
    /// тест с заданием свойств объекта с помощью AutoFixture
    /// </summary>
    [Test]
    public void Register_DoesNotThrowException_AutoFixtureVersion()
    {
        IFixture fixture = new Fixture();

        UserRegistrationData userRegistrationDataFixture
            = fixture.Create<UserRegistrationData>();

        userRegistrationDataFixture.Email = fixture.Create<string>() + "@mail.ru";

        Mock<IUserRepository> userRepositoryMock = new();
        Mock<IMessageRepository> messageRepositoryMock = new();
        Mock<IFriendRepository> friendRepositoryMock = new();

        userRepositoryMock.Setup(
            x => x.Create(It.IsAny<UserEntity>())).Returns(1);

        MessageService messageService = new(messageRepositoryMock.Object, userRepositoryMock.Object);
        UserService userService = new(userRepositoryMock.Object, friendRepositoryMock.Object, messageService);

        Assert.DoesNotThrow(
            () => userService.Register(userRegistrationDataFixture));
    }
}
