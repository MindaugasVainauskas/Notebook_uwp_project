using LittleNotebook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.Models
{
    public class Notebook
    {
        public List<Note> lstNotes { get; set; }

        public Notebook()
        {
            //Should open connection to DB here(Once I have a working DB -.-')

            //Get the current list of notes from DB
            lstNotes = MockDBService.GetNotes();
        }

        public void AddNote(Note note)
        {
            if (!lstNotes.Contains(note))
            {
                lstNotes.Add(note);
                MockDBService.Write(note);
            }
        }

        public void DeleteNote(Note note)
        {
            if (lstNotes.Contains(note))
            {
                lstNotes.Remove(note);
                MockDBService.Delete(note);
            }
        }

        public void UpdateNote(Note note)
        {
            MockDBService.Write(note);
        }
    }
}
