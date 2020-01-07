using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SimpleNotesServer.Data.Models.Notes
{
    public class SimpleNote : NamedEntityBase, IServerNote
    {
        [Required(ErrorMessage = "Дата создания должна быть указана")]
        public long Created { get; set; }

        [Required(ErrorMessage = "Дата изменения должна быть указана")]
        public long Changed { get; set; }

        [Required(ErrorMessage = "Содержимое заметки не должно быть пустым")]
        public string Content { get; set; }

        public SimpleNote() { }

        public SimpleNote(string name, string content)
        {
            Name = name;
            Content = content;

            Created = DateTimeOffset.Now.ToUnixTimeSeconds();
            Changed = Created;
        }

        public SimpleNote(SimpleNote newNote)
        {
            Name = newNote.Name;
            Content = newNote.Content;

            Created = DateTimeOffset.Now.ToUnixTimeSeconds();
            Changed = Created;
        }

        public void ChangeMe(SimpleNote changedNote)
        {
            Changed = DateTimeOffset.Now.ToUnixTimeSeconds();

            Name = changedNote.Name;
            Content = changedNote.Content;
        }
    }
}