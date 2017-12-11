using LittleNotebook.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;

namespace LittleNotebook.Data
{
    class AzureDBService
    {
        static IMobileServiceTable<Note> NoteTableObj = App.MobileService.GetTable<Note>();

        public static async Task<List<Note>> GetList()
        {
            List<Note> list = new List<Note>();
            list = await NoteTableObj.ToListAsync();
            Debug.WriteLine("List size is => "+list.Count);
            return list;
        }

       

        //This method saves data in the SQL DB hosted on Azure cloud.
        public static async void Write(Note note)
        {
            try
            {
                
                Note obj = new Note();
                obj.title = note.title;
                obj.noteBody = note.noteBody;
                await NoteTableObj.InsertAsync(obj);
                MessageDialog msgDialog = new MessageDialog("Note with title "+note.title+" has been saved successfully.");
                await msgDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                MessageDialog msgDialogError = new MessageDialog("Error : " + ex.ToString());
                await msgDialogError.ShowAsync();
            }
        }

        //This method should delete note from SQL DB on Azure
        public static async void Delete(Note note)
        {
            await NoteTableObj.DeleteAsync(note);
            Debug.WriteLine("DELETE note with title " + note.title);
        }
    }
}
