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

        //get list async call works.
        public static async Task<List<Note>> GetList()
        {
            List<Note> list = new List<Note>();
            list = await NoteTableObj.ToListAsync();
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
                MessageDialog msgDialog = new MessageDialog(note.title+" has been saved successfully.");
                await msgDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                MessageDialog msgDialogError = new MessageDialog("Error : " + ex.ToString());
                await msgDialogError.ShowAsync();
            }
        }

        //This method saves data in the SQL DB hosted on Azure cloud.
        public static async void Update(Note note)
        {
            try
            {
                await NoteTableObj.UpdateAsync(note); //Call updateAsync to update record instead of creating new one.
                MessageDialog msgDialog = new MessageDialog(note.title + " has been updated successfully.");
                await msgDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                
            }
        }

        //This method deletes note from SQL DB on Azure
        public static async void Delete(Note note)
        {
            await NoteTableObj.DeleteAsync(note);
        }
    }
}
