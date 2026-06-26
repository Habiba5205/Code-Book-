using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBook.Business.App.DTOs
{
    internal class NotificationDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string LinkUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
