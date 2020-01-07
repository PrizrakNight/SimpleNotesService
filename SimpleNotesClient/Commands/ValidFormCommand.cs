using SimpleNotesClient.Interfaces;

namespace SimpleNotesClient.Commands
{
    public abstract class ValidFormCommand<TViewModel> : ViewModelCommandBase<TViewModel>
        where TViewModel : IValidForm
    {
        public ValidFormCommand(TViewModel viewModel) : base(viewModel) { }

        public override bool CanExecute(object parameter) => viewModel.FormIsValid;
    }
}