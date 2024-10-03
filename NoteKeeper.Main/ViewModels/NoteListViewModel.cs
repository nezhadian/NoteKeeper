using NoteKeeper.Main.Helpers;
using NoteKeeper.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Main.ViewModels
{
    public class NoteListViewModel : SelectableCollectionViewModelBase<Note>
    {
        public NoteListViewModel()
        {
            Items = [
                new Note { Title = "Shopping List", Content = "Eggs, Milk, Bread" },
                new Note { Title = "Meeting Notes", Content = "Discuss project milestones" },
                new Note { Title = "To-Do", Content = "Finish report by Friday" },
                new Note { Title = "Ideas", Content = "New feature suggestions" },
                new Note { Title = "Travel Plans", Content = "Book flight tickets" },
            ];
        }
    }
}
