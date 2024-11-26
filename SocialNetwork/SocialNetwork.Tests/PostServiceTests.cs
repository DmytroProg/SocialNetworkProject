using Castle.Components.DictionaryAdapter.Xml;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Services;

namespace SocialNetwork.Tests;

public class PostServiceTests
{
    [Fact]
    public async Task GetPosts_ReturnCorrectPostsList()
    {
        var user = new User();
        var posts = new List<Post>()
        {
            new Post { Id = 1, LikesCount = 23, Description = "Test", User = user },
            new Post { Id = 2, LikesCount = 34, Description = "Test2", User = user },
        };
        var repository = TestHelper.CreateRepository(posts);
        var postService = new PostService(repository);

        var postsFromDb = await postService.GetPosts(false, 0, 2);
        
        Assert.Equal(posts.Count, postsFromDb.Count());
        Assert.Equal(posts, postsFromDb, new PostComparer());
    }
    
    [Fact]
    public async Task GetPostById_FirstPost_ReturnCorrectPost()
    {
        var user = new User();
        var posts = new List<Post>()
        {
            new Post { Id = 1, LikesCount = 23, Description = "Test", User = user },
            new Post { Id = 2, LikesCount = 34, Description = "Test2", User = user },
        };
        var repository = TestHelper.CreateRepository(posts);
        var postService = new PostService(repository);

        var post = await postService.GetPostById(1);
        
        Assert.Equal(posts.First(), post, new PostComparer());
    }
    
    [Fact]
    public async Task CreatePost_CorrectPost_AddPostToDB()
    {
        var user = new User();
        var post = new Post { LikesCount = 23, Description = "Test", User = user };
        var repository = TestHelper.CreateRepository([post]);
        var postService = new PostService(repository);

        var postFromDb = await postService.CreatePost(post);
        post.Id = 1;
        
        Assert.Equal(post, postFromDb, new PostComparer());
    } 
    
    [Theory]
    [InlineData(-1, "description")]
    [InlineData(10, "")]
    [InlineData(10, null)]
    public async Task CreatePost_IncorrectPost_ThrowException(int likes, string description)
    {
        var user = new User();
        var post = new Post { LikesCount = likes, Description = description, User = user };
        var repository = TestHelper.CreateRepository([post]);
        var postService = new PostService(repository);

        var exceptionHandler = () => postService.CreatePost(post);

        await Assert.ThrowsAsync<ArgumentException>(exceptionHandler);
    } 
}