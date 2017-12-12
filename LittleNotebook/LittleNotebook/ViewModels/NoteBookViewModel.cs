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
        NoteViewModel newNote;

        public NoteBookViewModel()
        {
            CreateList();           
        }

        //Creates the list.
        protected async void CreateList()
        {
            notebook = new Notebook();
            _SelectedIndex = -1;
            //Need to await for the list to return and then populate the listview from it.
            var list = await notebook.GetNotes();
            foreach (var note in list)
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
                { RaisePropertyChanged(nameof(SelectedNote)); }
            }
        }

        NoteViewModel tempNote;
        
        public NoteViewModel SelectedNote
        {
            //if note is within notes list, return that note.
            get
            {
                if (SelectedIndex == -1)
                {
                    if (tempNote == null)
                    {
                        tempNote = NewNote();
                    }
                    return tempNote;
                }
                else
                {
                    return _Notes[SelectedIndex];
                }
            }
        }

        public NoteViewModel NewNote()
        {
            newNote = new NoteViewModel();
            Debug.WriteLine("Debug->"+newNote.Title+" "+newNote.NoteBody);
            return newNote;
        }


        public void SaveNote()
        {
            var note = NewNote();
            if (tempNote != null)
            {
                note = tempNote;
                tempNote = null;
            }
            if (SelectedIndex < 0)
            {
                //adding new note into list doesnt work.
                Notes.Add(note);
                notebook.AddNote(note);
                SelectedIndex = Notes.IndexOf(note);
                CreateList();
                
            }
            else
            {
                //updating current note works.
                note.Title = SelectedNote.Title;
                note.NoteBody = SelectedNote.NoteBody;
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
            tempNote = null;
            SelectedIndex = -1;
        }

        void Note_OnNotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            notebook.UpdateNote((NoteViewModel)sender);
        }
    }
}
