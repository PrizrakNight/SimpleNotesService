using System;

namespace SimpleNotesClient.Models.Authentication
{
    [Serializable]
    public struct AutharizationSuccess
    {
        public string Access_Token { get; set; }

        public string Username { get; set; }
    }
}