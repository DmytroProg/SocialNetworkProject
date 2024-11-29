using SocialNetwork.Core.Helpers;
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
        var users = new List<User> { new User { Id = 1 }, new User { Id = 2 }, new User { Id = 3 } };
        var mockRepository = TestHelper.CreateRepository(users);
        
        var service = new UserService(mockRepository);

        // Act
        var result = await service.GetUsers(0, 2);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(users.Take(2), result, new UserComparer());
    }

    #endregion
    
    #region GetUserById

    [Fact]
    public async Task GetUserById_WithExistingUser_ReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, Nickname = "John" };
        var mockRepository = TestHelper.CreateRepository([user]);
        
        var service = new UserService(mockRepository);

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
        var user = new User { Id = 2, Nickname = "Alice" };
        var mockRepository = TestHelper.CreateRepository([user]);
       
        var service = new UserService(mockRepository);

        // Act
        var result = await service.GetUsersByName("Alice", 0, 10);

        // Assert
        Assert.Equal(user, result.First(), new UserComparer());
    }

    #endregion

    #region UpdateUser

    [Fact]
    public async Task UpdateUser_WithValidUserId_UpdatesAndReturnsUser()
    {
        // Arrange
        var user = new User { Id = 1, Nickname = "Name", Password = TestConstants.ValidPassword};
        var updatedUser = new User { Id = 1, Nickname = "UpdatedName", Password = TestConstants.ValidPassword };
        var mockRepository = TestHelper.CreateRepository([user]);
        
        var service = new UserService(mockRepository);

        // Act
        var newUser = await service.SignUp(user);
        updatedUser.Password = user.Password;
        mockRepository = TestHelper.CreateRepository([user, updatedUser]);
        service = new UserService(mockRepository);
        var result = await service.UpdateUser(1, newUser);

        // Assert
        Assert.Equal(updatedUser, result, new UserComparer());
    }
    
    #endregion

    #region LogIn

    [Fact]
    public async Task LogIn_WithValidUser_ReturnsUser()
    {
        // Arrange
        var user = new User { 
            Nickname = "John", 
            Password = TestConstants.ValidPassword,
        };
        var updatedUser = new User { 
            Id = 1,
            Nickname = "John",
            UserOnlineStatus = new OnlineStatus(DateTime.UtcNow),
        };
        var mockRepository = TestHelper.CreateRepository([user, updatedUser]);
        
        var service = new UserService(mockRepository);

        // Act
        await service.SignUp(user);
        updatedUser.Password = user.Password;
        mockRepository = TestHelper.CreateRepository([user, updatedUser]);
        var result = await service.LogIn("1", "1");

        // Assert
        Assert.Equal(user.Nickname, result.Nickname);
        Assert.Equal(user.Password, result.Password);
        Assert.True(updatedUser.IsLoggedIn);
    }
    
    #endregion

    #region LogOut

    [Fact]
    public async Task LogOut_WithValidUser_ChangesLoggedInStatus()
    {
        // Arrange
        var user = new User { 
            Nickname = "John", 
            Password = TestConstants.ValidPassword
        };
        var mockRepository = TestHelper.CreateRepository([user]);
        
        var service = new UserService(mockRepository);

        // Act
        await service.SignUp(user);
        await service.LogOut(1);
        user = await service.GetUserById(1);

        // Assert
        Assert.False(user.IsLoggedIn);
    }

    #endregion

    #region SignUp

    [Fact]
    public async Task SignUp_WithValidUser_CorrectResult()
    {
        // Arrange
        var user = new User { 
            Nickname = "John", 
            Password = TestConstants.ValidPassword
        };
        var expectedUser = new User
        {
            Id = 1,
            Nickname = "John",
            Password = TestConstants.ValidPassword
        };
        var mockRepository = TestHelper.CreateRepository([user, expectedUser]);
        
        var service = new UserService(mockRepository);

        // Act
        var newUser = await service.SignUp(user);
        expectedUser.Password = newUser.Password;
        user.Id = 1;

        // Assert
        Assert.Equal(expectedUser, newUser, new UserComparer());
    }

    #endregion
}