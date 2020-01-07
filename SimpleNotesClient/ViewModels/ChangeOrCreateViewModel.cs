using SimpleNotesClient.Commands.ViewsCommands;
using SimpleNotesClient.Interfaces;
using SimpleNotesClient.Models;
using SimpleNotesClient.ValidationRules;

namespace SimpleNotesClient.ViewModels
{
    public class ChangeOrCreateViewModel : BaseNotifyViewModel, IValidForm
    {
        public SimpleNote CurrentNote
        {
            get => _currentNote;
            set
            {
                _currentNote = value;
                OnPropertyChanged();
            }
        }

        public SimpleNote ChangedNote;

        public AcceptCommand AcceptCommand => _acceptCommand ?? (_acceptCommand = new AcceptCommand(this));

        public bool FormIsValid => NoteNameValidation.CheckValidate(CurrentNote.Name) &&
                                   NoteContentValidation.CheckValidate(CurrentNote.Content);

        private SimpleNote _currentNote;

        private AcceptCommand _acceptCommand;
    }
}