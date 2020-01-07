namespace SimpleNotesClient.Commands
{
    public abstract class ViewModelCommandBase<TViewModel> : CommandBase
    {
        protected readonly TViewModel viewModel;

        public ViewModelCommandBase(TViewModel viewModel) => this.viewModel = viewModel;
    }
}