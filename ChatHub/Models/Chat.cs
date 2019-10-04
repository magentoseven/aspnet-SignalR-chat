using System;
using System.ComponentModel.DataAnnotations;

namespace ChatHub.Models
{
    public class Chat
    {
        public Chat()
        {
            SentTime = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime SentTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

    }
}
