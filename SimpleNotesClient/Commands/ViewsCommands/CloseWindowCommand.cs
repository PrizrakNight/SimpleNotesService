using System.Windows;

namespace SimpleNotesClient.Commands.ViewsCommands
{
    public class CloseWindowCommand : CommandBase
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            if(parameter is Window window) window.Close();
        }
    }
}