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
        public ObservableCollection<T> Items { get; set; }

        private T? _selectedItem;
        public T? SelectedItem
        {
            get => _selectedItem ?? Items?[0];
            set
            {
                if (value is null)
                    return;

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
