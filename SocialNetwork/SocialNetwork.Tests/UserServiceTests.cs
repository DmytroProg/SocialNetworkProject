using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Services;

namespace SocialNetwork.Tests;

public class UserServiceTests
{
    
    #region GetUsers
    
    [Fact]
    public async Task GetUsers_ReturnsCorrectUserList()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var users = new List<User> { new User { Id = 1 }, new User { Id = 2 }, new User { Id = 3 } };
        mockContext.Setup(ctx => ctx.Set<User>()).ReturnsDbSet(users);
        mockRepository.Setup(repo => repo.GetAll<User>()).Returns(mockContext.Object.Set<User>());
    
        var service = new UserService(mockRepository.Object);

        // Act
        var result = await service.GetUsers(0, 2);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(users.Take(2), result);
    }

    #endregion
    
    #region GetUserById

    [Fact]
    public async Task GetUserById_WithExistingUser_ReturnsUser()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var user = new User { Id = 1, Nickname = "John" };
        mockContext.Setup(ctx => ctx.Set<User>()).ReturnsDbSet([user]);
        mockRepository.Setup(repo => repo.GetById<User>(1)).ReturnsAsync(user);
    
        var service = new UserService(mockRepository.Object);

        // Act
        var result = await service.GetUserById(1);

        // Assert
        Assert.Equal(user, result);
    }

    #endregion

    #region GetUserByName

    [Fact]
    public async Task GetUserByName_WithName_ReturnsCorrectUser()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var user = new User { Id = 2, Nickname = "Alice" };
        mockContext.Setup(ctx => ctx.Set<User>()).ReturnsDbSet([user]);
        mockRepository.Setup(repo => repo.GetAll<User>()).Returns(mockContext.Object.Set<User>());

        var service = new UserService(mockRepository.Object);

        // Act
        var result = await service.GetUserByName("Alice");

        // Assert
        Assert.Equal(user, result);
    }

    #endregion

    #region UpdateUser

    [Fact]
    public async Task UpdateUser_WithValidUserId_UpdatesAndReturnsUser()
    {
        // Arrange
        var mockRepository = new Mock<IRepository>();
        var user = new User { Id = 1, Nickname = "UpdatedName", Password = TestConstants.ValidPassword};
        mockRepository.Setup(repo => repo.Update(user, 1)).ReturnsAsync(user);
    
        var service = new UserService(mockRepository.Object);

        // Act
        var result = await service.UpdateUser(1, user);

        // Assert
        Assert.Equal(user, result);
    }
    
    #endregion

    #region LogIn

    [Fact]
    public async Task LogIn_WithValidUser_ReturnsUser()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var user = new User { 
            Id = 1, 
            Nickname = "John", 
            Password = TestConstants.ValidPassword
        };
        mockContext.Setup(ctx => ctx.Set<User>()).ReturnsDbSet([user]);
        mockRepository.Setup(repo => repo.GetAll<User>()).Returns(mockContext.Object.Set<User>());
    
        var service = new UserService(mockRepository.Object);

        // Act
        var result = await service.LogIn(user);

        // Assert
        Assert.Equal(user.Nickname, result.Nickname);
        Assert.Equal(user.Password, result.Password);
    }
    
    #endregion

    #region LogOut

    [Fact]
    public async Task LogOut_WithValidUser_ChangesLoggedInStatus()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var user = new User { 
            Id = 1, 
            Nickname = "John", 
            Password = TestConstants.ValidPassword
        };
        mockContext.Setup(ctx => ctx.Set<User>()).ReturnsDbSet([user]);
        mockRepository.Setup(repo => repo.GetAll<User>()).Returns(mockContext.Object.Set<User>());
    
        var service = new UserService(mockRepository.Object);

        // Act
        await service.LogOut(user);
        user = await service.GetUserById(1);

        // Assert
        Assert.False(user.IsLoggedIn);
    }

    #endregion

    #region SignUp

    [Fact]
    public async Task SignUp_WithValidUser_()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockRepository = new Mock<IRepository>();
        var user = new User { 
            Nickname = "John", 
            Password = TestConstants.ValidPassword
        };
        mockContext.Setup(ctx => ctx.Set<User>()).ReturnsDbSet([user]);
        mockRepository.Setup(repo => repo.GetAll<User>()).Returns(mockContext.Object.Set<User>());
    
        var service = new UserService(mockRepository.Object);

        // Act
        var newUser = await service.SignUp(user);

        // Assert
        Assert.Equal(user, newUser);
    }

    #endregion
}