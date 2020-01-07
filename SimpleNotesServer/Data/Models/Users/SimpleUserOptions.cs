using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SimpleNotesServer.Data.Models.Users
{
    public class SimpleUserOptions : EntityBase
    {
        [JsonIgnore]
        public SimpleUser OwnerOptions { get; set; }

        [JsonIgnore]
        public int OwnerKey { get; set; }

        [Required(ErrorMessage = "Пользователь обязан иметь аватар")]
        public string AvatarURL { get; set; } =
            "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/e4d05b6d-6a31-4866-a76a-c9f999903bbd/d89a5l0-aa7a927e-0a03-409a-9b0c-3e81960062b0.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcL2U0ZDA1YjZkLTZhMzEtNDg2Ni1hNzZhLWM5Zjk5OTkwM2JiZFwvZDg5YTVsMC1hYTdhOTI3ZS0wYTAzLTQwOWEtOWIwYy0zZTgxOTYwMDYyYjAucG5nIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.k_b3yHLPdAh92KtG67jCMf2rPPlCLyeGNKDZAxlPNkk";
    }
}