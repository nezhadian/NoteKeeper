using NoteKeeper.Main.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Main.Helpers
{
    public abstract class SelectableCollectionViewModelBase<T> : BindableBase where T : class
    {


        //props
        private ObservableCollection<T> _items;
        public ObservableCollection<T> Items { get => _items; set => SetProperty(ref _items,value); }



        private T? _selectedItem;

        public T? SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnSelectedItemChanged(value);
            }
        }

        protected SelectableCollectionViewModelBase()
        {
            Items = new ObservableCollection<T>();
        }

        public virtual void OnSelectedItemChanged(T value) { }
    }
}
