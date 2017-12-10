using LittleNotebook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.ViewModels
{
    public class NoteBookViewModel : NotificationHelper
    {
        Notebook notebook;
        NoteViewModel tempNote;
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
            //if note is within notes list, return that note.
            //get { return (SelectedIndex >= 0) ? _Notes[SelectedIndex] : NewNote(); }
            get
            {
                if (SelectedIndex < 0)
                {
                    return NewNote();
                }
                else
                {
                    return _Notes[SelectedIndex];
                }
            }
            set
            {
                NoteInFocus = value;
                NoteInFocus.Title = value.Title;
                NoteInFocus.NoteBody = value.NoteBody;
            }
        }

        public NoteViewModel NewNote()
        {
            tempNote = new NoteViewModel();
            Debug.WriteLine("Debug->"+tempNote.Title+" "+tempNote.NoteBody);
            return tempNote;
        }


        public void SaveNote()
        {
            var note = NewNote();
            if (SelectedIndex < 0)
            {
                //adding new note into list doesnt work.
                Notes.Add(note);
                notebook.AddNote(note);
                SelectedIndex = Notes.IndexOf(note);
            }
            else
            {
                //updating current note works.
                note.Title = NoteInFocus.Title;
                note.NoteBody = NoteInFocus.NoteBody;
                notebook.UpdateNote(note);
                Debug.WriteLine("Current note has been saved!");
            }           
        }

        //Delete note from list. Works.
        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var note = Notes[SelectedIndex];
                Notes.RemoveAt(SelectedIndex);
                notebook.DeleteNote(note);
                SelectedIndex = -1;
            }
        }

        //Set notebook up for new note. seems to work now
        public void New()
        {
            SelectedIndex = -1;
        }

        void Note_OnNotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            notebook.UpdateNote((NoteViewModel)sender);
        }
    }
}
