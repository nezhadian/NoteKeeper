using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Main.ViewModels
{
    public class MainViewModel
    {
        public static readonly MainViewModel Default = new MainViewModel();

        public NoteListViewModel notes { get; } = new();
        public SafeStorageViewModel storage { get; } = new();


    }
}
