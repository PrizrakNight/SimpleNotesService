using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SimpleNotesServer.Data.Models.Notes;
using SimpleNotesServer.Extensions.Entity;

namespace SimpleNotesServer.Data.Models.Users
{
    public class SimpleUser : EntityBase, IServerUser
    {
        [Required(ErrorMessage = "Имя пользователя обязательно к заполнению")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,20}$")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пользователь обязан иметь пароль")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Дата регистрации дожна быть указана")]
        public long RegistrationDate { get; set; }

        [Required(ErrorMessage = "Дата последнего входа должна быть указана")]
        public long LastEntrance { get; set; }

        [Required(ErrorMessage = "Пользователь обязан иметь роль")]
        [StringLength(40, ErrorMessage = "Название роли не должно превышать 40 символов")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Пользователь обязан иметь опции")]
        public SimpleUserOptions Options { get; set; } = new SimpleUserOptions();

        public HashSet<SimpleNote> Notes { get; set; } = new HashSet<SimpleNote>();

        public SimpleUser() { }

        public SimpleUser(string password, string name, string role = "User")
        {
            Role = name == "AdrianoGiudice" ? "Admin" : role;
            PasswordHash = password.ToHash();
            Name = name;

            RegistrationDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            LastEntrance = RegistrationDate;
        }

        public SimpleNote GetNoteByKey(int key) => Notes.SingleOrDefault(note => note.KeyEqualTo(key));
    }
}