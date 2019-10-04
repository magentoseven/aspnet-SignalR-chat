using ChatHub.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ChatHub.DAL
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("name=ChatContext")
        {
        }

        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}