using SimpleNotesClient.ViewModels;
using SimpleNotesClient.WebApiQueries.Authorized;

namespace SimpleNotesClient.Commands.NoteCommands
{
    public class DeleteNoteCommand : ViewModelCommandBase<WorkplaceViewModel>
    {
        public DeleteNoteCommand(WorkplaceViewModel viewModel) : base(viewModel) { }

        public override bool CanExecute(object parameter) => viewModel.SelectedNote != default;

        public override async void Execute(object parameter)
        {
            AuthorizedDeleteQuery deleteQuery = new AuthorizedDeleteQuery("notes", "delete", viewModel.SelectedNote.Key.ToString());

            await deleteQuery.ExecuteAsync();

            viewModel.UpdateTest();
        }
    }
}