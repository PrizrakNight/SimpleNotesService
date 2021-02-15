using SimpleNotesServer.Sdk;
using SimpleNotesServer.Sdk.Models.Identification;
using SimpleNotesServer.Sdk.Models.Note;
using SimpleNotesServer.Sdk.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleNotes.Client.ConsoleApp
{
    class Program
    {
        private static readonly NoteServiceClient _noteServiceClient = new NoteServiceClient("https://localhost:44330/api");

        private static readonly Dictionary<string, Func<Task>> _actions = new Dictionary<string, Func<Task>>
        {
            {"Get", GetNotesAsync },
            {"Create", CreateNoteAsync },
            {"Delete", DeleteNoteAsync },
            {"UpdateN", UpdateNoteAsync },
            {"UpdateP", UpdateProfileAsync },
            {"GetP", GetProfileAsync }
        };

        private static async Task GetProfileAsync()
        {
            var profile = await _noteServiceClient.ProfileClient.GetProfileAsync();

            Console.WriteLine($"{profile.Username} - {profile.AvatarUrl}");
        }

        private static async Task UpdateProfileAsync()
        {
            var username = GetInputWithMessage("Enter new username");
            var avatar = GetInputWithMessage("Enter new avatar url");

            await _noteServiceClient.ProfileClient.UpdateProfileAsync(new UserModel
            {
                AvatarUrl = avatar,
                Username = username
            });

            Console.WriteLine("Successful");
        }

        private static async Task UpdateNoteAsync()
        {
            var noteKey = GetInputWithMessage("Enter note key");

            if (int.TryParse(noteKey, out int key))
            {
                var noteName = GetInputWithMessage("Enter note name");
                var content = GetInputWithMessage("Enter note content");

                await _noteServiceClient.NoteClient.UpdateNoteAsync(new NoteModel
                {
                    Key = key,
                    Name = noteName,
                    Content = content
                });
            }
            else
            {
                Console.WriteLine("The specified value is not a key");

                await UpdateNoteAsync();
            }
        }

        private static async Task DeleteNoteAsync()
        {
            var noteKey = GetInputWithMessage("Enter note key");

            if (int.TryParse(noteKey, out int key))
            {
                await _noteServiceClient.NoteClient.DeleteNoteAsync(key);

                Console.WriteLine("Successful!");
            }
            else
            {
                Console.WriteLine("The specified value is not a key");

                await DeleteNoteAsync();
            }
        }

        private static async Task CreateNoteAsync()
        {
            var noteName = GetInputWithMessage("Enter note name");
            var content = GetInputWithMessage("Enter note content");

            var createdNote = await _noteServiceClient.NoteClient.CreateNoteAsync(new NoteModel
            {
                Name = noteName,
                Content = content
            });

            Console.WriteLine("Successful!");
        }

        private static async Task GetNotesAsync()
        {
            var response = await _noteServiceClient.NoteClient.GetNotesAsync();

            foreach (var detailedNote in response)
            {
                Console.WriteLine($"[{detailedNote.Key}]: {detailedNote.Name} - {detailedNote.Content} " +
                    $"| CR:{detailedNote.Created};CA:{detailedNote.Changed}");
            }
        }

        private static async Task Main(string[] args)
        {
            var userInput = GetInputWithMessage("R - registration; L - login");

            if (userInput.Equals("R", StringComparison.OrdinalIgnoreCase))
                await RegisterAsync();

            else await LoginAsync();

            PrintCommands();

            while (true)
            {
                var command = Console.ReadLine();

                if (_actions.TryGetValue(command, out Func<Task> action))
                    await action.Invoke();

                else Console.WriteLine("Unknown action, try again.");
            }
        }

        private static void PrintCommands()
        {
            Console.WriteLine("Available commands: ");

            foreach (var keyValuePair in _actions)
            {
                Console.WriteLine(keyValuePair.Key);
            }
        }

        private static async Task RegisterAsync()
        {
            var username = GetInputWithMessage("Enter your username");
            var password = GetInputWithMessage("Enter your password");
            var confirmPassword = GetInputWithMessage("Confirm password");
            var avatar = GetInputWithMessage("Enter avatar url [Can be empty]");

            await _noteServiceClient.RegisterAsync(new RegistrationModel
            {
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword,
                AvatarUrl = avatar
            });

            Console.WriteLine("Successful registration");
        }

        private static async Task LoginAsync()
        {
            var userInput = GetInputWithMessage("Log in using password;username or access_token");

            if (userInput.Contains(";"))
            {
                var credentials = userInput.Split(';');

                await _noteServiceClient.LoginAsync(credentials[0], credentials[1]);
            }
            else await _noteServiceClient.LoginAsync(userInput);
        }

        private static string GetInputWithMessage(string message)
        {
            Console.WriteLine(message);

            return Console.ReadLine();
        }
    }
}
