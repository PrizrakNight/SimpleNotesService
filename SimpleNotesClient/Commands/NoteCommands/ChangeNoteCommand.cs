using SimpleNotesClient.Managers;
using SimpleNotesClient.ViewModels;
using SimpleNotesClient.WebApiQueries.Authorized;

namespace SimpleNotesClient.Commands.NoteCommands
{
    public class ChangeNoteCommand : ViewModelCommandBase<WorkplaceViewModel>
    {
        public ChangeNoteCommand(WorkplaceViewModel viewModel) : base(viewModel) { }

        public override bool CanExecute(object parameter) => viewModel.SelectedNote != default;

        public override async void Execute(object parameter)
        {
            if (WindowsManager.ShowChangeOrCreateDialog(model => model.CurrentNote = viewModel.SelectedNote) == true)
            {
                AuthorizedPutQuery putQuery = new AuthorizedPutQuery("notes", "change", viewModel.SelectedNote.Key.ToString());

                putQuery.PutData = viewModel.SelectedNote;

                await putQuery.ExecuteAsync();
            }

            viewModel.UpdateTest();
        }
    }
}