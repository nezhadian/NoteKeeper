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

        public ICommand AddNewNoteCommand { get; set; }
        public Command DeleteNoteCommand { get; set; }
        public ICommand SaveAllCommand { get; set; }

        public NoteListViewModel()
        {
            Items = [];

            AddNewNoteCommand = new Command(AddNewCmd_Execute);

            DeleteNoteCommand = new Command(DeleteCmd_Execute, DeleteCmd_CanExecute);

            SaveAllCommand = new Command(SaveItems);
        }

        private void AddNewCmd_Execute()
        {
            if (Items.Count == 0 && SelectedItem is not null)
            {
                Items.Add(SelectedItem);
                DeleteNoteCommand.ChangeCanExecute();
                return;
            }

            SelectedItem = AddNewEmptyNote();
        }

        private bool DeleteCmd_CanExecute(object arg)
        {
            return Items.Count != 0 && SelectedItem is not null;
        }
        private void DeleteCmd_Execute(object obj)
        {
            var index = Items.IndexOf(SelectedItem);
            Items.RemoveAt(index);
            index = Math.Min(index, Items.Count - 1);
            SelectedItem = index == -1 ? new Note() : Items[index];
        }



        public void SaveItems()
        {
            var json = JsonConvert.SerializeObject(Items.ToList(), Formatting.Indented);
            MainViewModel.Default.storage.SaveAndEncrypt(json);
        }
        public void LoadItems()
        {
            var json = MainViewModel.Default.storage.ReadAndDecrypt();
            if (json is null)
                return;

            var loadedItems = JsonConvert.DeserializeObject<IList<Note>>(json);
            Items = new(loadedItems ?? []);
        }

        private Note AddNewEmptyNote()
        {
            var note = new Note();
            Items.Add(note);
            return note;
        }

        public override void OnSelectedItemChanged(Note value)
        {
            DeleteNoteCommand.ChangeCanExecute();
        }
    }

}
