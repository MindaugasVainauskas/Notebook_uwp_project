using LittleNotebook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.ViewModels
{
    public class NoteBookViewModel : NotificationHelper
    {
        Notebook notebook;

        public NoteBookViewModel()
        {
            notebook = new Notebook();
            _SelectedIndex = -1;

            foreach (var note in notebook.lstNotes)
            {
                var n = new NoteViewModel(note);
                n.PropertyChanged += Note_OnNotifyPropertyChanged;
                _Notes.Add(n);
            }
        }

        ObservableCollection<NoteViewModel> _Notes = new ObservableCollection<NoteViewModel>();

        public ObservableCollection<NoteViewModel> Notes
        {
            get { return _Notes; }
            set { SetProperty(ref _Notes, value); }
        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(NoteInFocus)); }
            }
        }

        public NoteViewModel NoteInFocus
        {
            get { return (_SelectedIndex >= 0) ? _Notes[_SelectedIndex] : null; }
        }

        public void Add()
        {
            var note = new NoteViewModel();
            note.PropertyChanged += Note_OnNotifyPropertyChanged;
            Notes.Add(note);
            notebook.AddNote(note);
            SelectedIndex = Notes.IndexOf(note);
        }

        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var note = Notes[SelectedIndex];
                Notes.RemoveAt(SelectedIndex);
                notebook.DeleteNote(note);
            }
        }

        public void New()
        {
            var newNote = new NoteViewModel();
        }

        void Note_OnNotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            notebook.UpdateNote((NoteViewModel)sender);
        }
    }
}
