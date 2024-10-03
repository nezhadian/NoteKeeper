using NoteKeeper.Main.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeper.Main.Models
{
    public class Note : BindableBase
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string? _title;
        public string? Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private object? _content;
        public object? Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { SetProperty(ref _creationDate, value); }
        }

        // Constructor to initialize 
        public Note()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }


}
