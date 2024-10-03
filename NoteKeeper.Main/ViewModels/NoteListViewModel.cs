using DORVPN.ExtendedControls;
using Newtonsoft.Json;
using NoteKeeper.Main.Helpers;
using NoteKeeper.Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace NoteKeeper.Main.ViewModels
{
    public class NoteListViewModel : SelectableCollectionViewModelBase<Note>
    {
        private const string FileName = "notes.json";

        public ICommand  AddNewNoteCommand { get; set; }
        public ICommand SaveAllCommand { get; set; }

        public NoteListViewModel()
        {
            Items = [];
            LoadItems();

            AddNewNoteCommand = new Command(() =>
            {
                var note = new Note();
                Items.Add(note);
                SelectedItem = note;
            });

            SaveAllCommand = new Command(SaveItems);
        }

        public void SaveItems()
        {
            var json = JsonConvert.SerializeObject(Items.ToList(), Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
        public void LoadItems()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                var loadedItems = JsonConvert.DeserializeObject<IList<Note>>(json);
                Items = new (loadedItems ?? []);
            }
        }
    }

}
