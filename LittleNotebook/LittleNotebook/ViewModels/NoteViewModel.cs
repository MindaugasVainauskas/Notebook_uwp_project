using LittleNotebook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.ViewModels
{
    public class NoteViewModel: NotificationHelper<Note>
    {
        public NoteViewModel(Note note = null): base(note) { }

        public string Title
        {
            get { return This.title; }
            set { SetProperty(This.title, value, () => This.title = value); }
        }

        public string NoteBody
        {
            get { return This.noteBody; }
            set { SetProperty(This.noteBody, value, () => This.noteBody = value); }
        }
    }
}
