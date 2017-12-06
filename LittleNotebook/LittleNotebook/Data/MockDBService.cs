using LittleNotebook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.Data
{
    
    class MockDBService
    {
        public static List<Note> GetNotes()
        {
            Debug.WriteLine("GET the list of notes");
            return new List<Note>()
            {
                new Note() { title="Note 1", noteBody="This is note one"},
                new Note() { title="Note 2", noteBody="This is note two"},
                new Note() { title="Note 3", noteBody="This is note three"},
                new Note() { title="Note 4", noteBody="This is note four"},
                new Note() { title="Note 5", noteBody="This is note five"}
            };
        }

        public static void Write(Note note)
        {
            Debug.WriteLine("INSERT new note with title " + note.title);
        }

        public static void Delete(Note note)
        {
            Debug.WriteLine("DELETE note with title " + note.title);
        }
    }
}
