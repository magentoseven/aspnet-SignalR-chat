using ChatHub.Models;
using System.Collections.Generic;

namespace ChatHub.DAL
{
    public class ChatInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ChatContext>
    {
        protected override void Seed(ChatContext context)
        {
            var chatList = new List<Chat>
            {
                new Chat { Text = "Halo" },
                new Chat { Text = "Hola" },
                new Chat { Text = "Bonjour" },
                new Chat { Text = "Shalom" }
            };
            chatList.ForEach(c => context.Chats.Add(c));
            context.SaveChanges();
        }
    }
}