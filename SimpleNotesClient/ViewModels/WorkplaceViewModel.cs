using System;
using System.Collections.ObjectModel;
using SimpleNotesClient.Commands.NoteCommands;
using SimpleNotesClient.Commands.ViewsCommands;
using SimpleNotesClient.Managers;
using SimpleNotesClient.Models;

namespace SimpleNotesClient.ViewModels
{
    public class WorkplaceViewModel : BaseNotifyViewModel
    {
        public SimpleNote SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SimpleNote> SimpleNotes
        {
            get => _simpleNotes;
            set
            {
                _simpleNotes = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                OnPropertyChanged();
            }
        }

        public CloseWindowCommand CloseWindow => _closeWindow ?? (_closeWindow = new CloseWindowCommand());

        public CreateNoteCommand CreateNote => _createNote ?? (_createNote = new CreateNoteCommand(this));

        public ChangeNoteCommand ChangeNote => _changeNote ?? (_changeNote = new ChangeNoteCommand(this));

        public DeleteNoteCommand DeleteNote => _deleteNote ?? (_deleteNote = new DeleteNoteCommand(this));

        private CreateNoteCommand _createNote;

        private DeleteNoteCommand _deleteNote;

        private ChangeNoteCommand _changeNote;

        private CloseWindowCommand _closeWindow;

        private ObservableCollection<SimpleNote> _simpleNotes;

        private SimpleNote _selectedNote;

        private string _username;
        private string _avatar;

        public WorkplaceViewModel()
        {
            Username = AuthManager.AutharizationSuccess.Username;

            UpdateTest();
        }

        public void UpdateTest()
        {
            LoadNotesAsync();
            LoadAvatarAsync();
        }

        private async void LoadNotesAsync()
        {
            try
            {
                SimpleNotes = new ObservableCollection<SimpleNote>(await DataManager.GetAllNotesAsync());
            }
            catch (Exception e)
            {
                //Do Something...
            }
        } 

        private async void LoadAvatarAsync() => Avatar = await DataManager.GetAvatar();
    }
}