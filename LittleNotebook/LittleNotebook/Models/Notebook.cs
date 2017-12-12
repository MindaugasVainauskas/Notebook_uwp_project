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

        public async Task<List<Note>> GetNotes()
        {
            lstNotes = await AzureDBService.GetList();

            return lstNotes;
        }

        public Notebook()
        {
        }

        //This method now saves the notes properly in SQL DB hosted in Azure.
        public void AddNote(Note note)
        {
            if (!lstNotes.Contains(note))
            {
                lstNotes.Add(note);
                AzureDBService.Write(note);
            }
        }

        public void DeleteNote(Note note)
        {
            if (lstNotes.Contains(note))
            {
                lstNotes.Remove(note);
                AzureDBService.Delete(note);
            }
        }

        public void UpdateNote(Note note)
        {
            AzureDBService.Update(note);
        }
    }
}
