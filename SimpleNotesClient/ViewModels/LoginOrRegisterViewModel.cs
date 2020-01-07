using System.Windows;
using SimpleNotesClient.Commands.AuthCommands;
using SimpleNotesClient.Interfaces;
using SimpleNotesClient.Managers;

namespace SimpleNotesClient.ViewModels
{
    public class LoginOrRegisterViewModel : BaseNotifyViewModel, IValidForm
    {
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (!string.IsNullOrEmpty(value)) ErrorBoxVisibility = Visibility.Visible;
                else ErrorBoxVisibility = Visibility.Hidden;

                _errorMessage = value;

                OnPropertyChanged();
            }
        }

        public Visibility ErrorBoxVisibility
        {
            get => _errorBoxVisibility;
            set
            {
                _errorBoxVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool FormIsValid => AuthManager.PasswordIsValid(Password) && AuthManager.UsernameIsValid(Username);

        public AuthCommand RegisterCommand => _registerCommand ?? (_registerCommand = new AuthCommand(this));

        public AuthCommand LoginCommand => _loginCommand ?? (_loginCommand = new AuthCommand(this));

        private AuthCommand _registerCommand;
        private AuthCommand _loginCommand;

        private Visibility _errorBoxVisibility = Visibility.Hidden;

        private string _username;
        private string _password;
        private string _errorMessage;

        public void ResetError() => ErrorMessage = string.Empty;
    }
}