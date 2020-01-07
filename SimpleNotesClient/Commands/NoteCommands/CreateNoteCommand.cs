using SimpleNotesClient.Managers;
using SimpleNotesClient.Models;
using SimpleNotesClient.ViewModels;
using SimpleNotesClient.WebApiQueries.Authorized;

namespace SimpleNotesClient.Commands.NoteCommands
{
    public class CreateNoteCommand : ViewModelCommandBase<WorkplaceViewModel>
    {
        public CreateNoteCommand(WorkplaceViewModel viewModel) : base(viewModel) { }

        public override bool CanExecute(object parameter) => true;

        public override async void Execute(object parameter)
        {
            if ((bool)WindowsManager.ShowChangeOrCreateDialog(changeModel =>
               changeModel.CurrentNote = new SimpleNote()))
            {
                AuthorizedPostQuery postQuery = new AuthorizedPostQuery("notes", "add")
                    {PostData = WindowsManager.CurrentDialog.GetContext<ChangeOrCreateViewModel>().CurrentNote};

                await postQuery.ExecuteAsync();

                viewModel.UpdateTest();

                //if(viewModel.SimpleNotes == default) viewModel.SimpleNotes = new ObservableCollection<SimpleNote>();

                //SimpleNote addedNote = WindowsManager.CurrentDialog.GetContext<ChangeOrCreateViewModel>().CurrentNote;

                //viewModel.SimpleNotes.Add(addedNote);

                //DataManager.CreateNote(addedNote);
            }
        }
    }
}