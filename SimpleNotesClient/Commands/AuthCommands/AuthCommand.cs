using SimpleNotesClient.ViewModels;
using SimpleNotesClient.Managers;
using SimpleNotesClient.Models.Authentication;
using SimpleNotesClient.Views;
using SimpleNotesClient.WebApiQueries;

namespace SimpleNotesClient.Commands.AuthCommands
{
    public class AuthCommand : ValidFormCommand<LoginOrRegisterViewModel>
    {
        public AuthCommand(LoginOrRegisterViewModel viewModel) : base(viewModel) { }

        public override async void Execute(object parameter)
        {
            try
            {
                AutharizationSuccess success =
                    await new AuthQueryBase(viewModel.Username, viewModel.Password, (string) parameter).Authorize();

                AuthManager.AutharizationSuccess = success;

                WindowsManager.SetWindow<Workplace, WorkplaceViewModel>();

                viewModel.ResetError();
            }
            catch (FlurlHttpException exception)
            {
                viewModel.ErrorMessage = await exception.GetResponseStringAsync();
            }
        }
    }
}