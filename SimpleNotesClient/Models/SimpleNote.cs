using System;

namespace SimpleNotesClient.Models
{
    public class SimpleNote
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateChanged { get; set; } = DateTime.Now;
    }
}