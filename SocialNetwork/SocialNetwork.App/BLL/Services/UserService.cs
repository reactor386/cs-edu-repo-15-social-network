//-
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Entities;


namespace SocialNetwork.BLL.Services;

public class UserService
{
    MessageService _messageService;
    IUserRepository _userRepository;
    IFriendRepository _friendRepository;

    public UserService(IUserRepository userRepository,
        IFriendRepository friendRepository, MessageService messageService)
    {
        // _userRepository = new UserRepository();
        // _friendRepository = new FriendRepository();
        // _messageService = new MessageService();

        _userRepository = userRepository;
        _friendRepository = friendRepository;

        _messageService = messageService;
    }


    public void Register(UserRegistrationData userRegistrationData)
    {
        if (String.IsNullOrEmpty(userRegistrationData.FirstName))
            throw new ArgumentNullException();

        if (String.IsNullOrEmpty(userRegistrationData.LastName))
            throw new ArgumentNullException();

        if (String.IsNullOrEmpty(userRegistrationData.Password))
            throw new ArgumentNullException();

        if (String.IsNullOrEmpty(userRegistrationData.Email))
            throw new ArgumentNullException();

        if (userRegistrationData.Password.Length < 8)
            throw new ArgumentNullException();

        // System.ComponentModel.DataAnnotations
        if (!new EmailAddressAttribute().IsValid(userRegistrationData.Email))
            throw new ArgumentNullException();

        if (_userRepository.FindByEmail(userRegistrationData.Email) != null)
            throw new ArgumentNullException();


        var userEntity = new UserEntity()
        {
            firstname = userRegistrationData.FirstName,
            lastname = userRegistrationData.LastName,
            password = userRegistrationData.Password,
            email = userRegistrationData.Email
        };

        if (this._userRepository.Create(userEntity) == 0)
            throw new Exception();
    }


    public User Authenticate(UserAuthenticationData userAuthenticationData)
    {
        var findUserEntity = _userRepository.FindByEmail(userAuthenticationData.Email);
        
        if (findUserEntity is null)
            throw new UserNotFoundException();

        if (findUserEntity.password != userAuthenticationData.Password)
            throw new WrongPasswordException();

        return ConstructUserModel(findUserEntity);
    }


    public User FindByEmail(string email)
    {
        var findUserEntity = _userRepository.FindByEmail(email);

        if (findUserEntity is null)
            throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }


    public User FindById(int id)
    {
        var findUserEntity = _userRepository.FindById(id);

        if (findUserEntity is null)
            throw new UserNotFoundException();

        return ConstructUserModel(findUserEntity);
    }


    public void Update(User user)
    {
        var updatableUserEntity = new UserEntity()
        {
            id = user.Id,
            firstname = user.FirstName,
            lastname = user.LastName,
            password = user.Password,
            email = user.Email,
            photo = user.Photo,
            favorite_movie = user.FavoriteMovie,
            favorite_book = user.FavoriteBook
        };

        if (this._userRepository.Update(updatableUserEntity) == 0)
            throw new Exception();
    }


    public void DeleteById(int id)
    {
        var findUserEntity = _userRepository.FindById(id);

        if (findUserEntity != null)
        {
            _userRepository.DeleteById(id);
        }
    }


    public IEnumerable<User> GetFriendsByUserId(int userId)
    {
        return _friendRepository.FindAllByUserId(userId)
                .Select(friendsEntity => FindById(friendsEntity.friend_id));
    }


    public void AddFriend(UserAddingFriendData userAddingFriendData)
    {
        var findUserEntity = _userRepository.FindByEmail(userAddingFriendData.FriendEmail);
        
        if (findUserEntity is null)
            throw new UserNotFoundException();

        var friendEntity = new FriendEntity()
        {
            user_id = userAddingFriendData.UserId,
            friend_id = findUserEntity.id
        };

        if (this._friendRepository.Create(friendEntity) == 0)
            throw new Exception();
    }


    private User ConstructUserModel(UserEntity userEntity)
    {
        var incomingMessages = _messageService.GetIncomingMessagesByUserId(userEntity.id);
        var outgoingMessages = _messageService.GetOutcomingMessagesByUserId(userEntity.id);
        var friends = GetFriendsByUserId(userEntity.id);

        return new User(userEntity.id,
                        userEntity.firstname,
                        userEntity.lastname,
                        userEntity.password,
                        userEntity.email,
                        userEntity.photo,
                        userEntity.favorite_movie,
                        userEntity.favorite_book,
                        incomingMessages,
                        outgoingMessages,
                        friends
                        );
    }
}
