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
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace NoteKeeper.Main.ViewModels
{
    public class NoteListViewModel : SelectableCollectionViewModelBase<Note>
    {
        private const string FileName = "notes.json";

        public NoteListViewModel()
        {
            Items = [];
            LoadItems();
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
