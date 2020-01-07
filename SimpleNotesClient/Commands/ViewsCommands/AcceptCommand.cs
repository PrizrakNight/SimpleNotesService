using System.Windows;
using SimpleNotesClient.ViewModels;

namespace SimpleNotesClient.Commands.ViewsCommands
{
    public class AcceptCommand : ValidFormCommand<ChangeOrCreateViewModel>
    {
        public AcceptCommand(ChangeOrCreateViewModel viewModel) : base(viewModel) { }

        public override void Execute(object parameter)
        {
            if (parameter is Window window) window.DialogResult = true;
        }
    }
}