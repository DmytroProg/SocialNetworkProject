using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Storage.Data
{
    public class SocialNetworkContext: DbContext
    {

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> option) : base(option) 
        { 
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
