using System;
using System.Collections.Generic;
using System.Text;

namespace TaskOrganizerLibrary.Model
{
    public class LibraryEventsModel
    {
        public string Event { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Finished { get; set; }
        public bool Activated { get; set; }
    }
}
