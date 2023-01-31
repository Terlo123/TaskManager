using System;
using System.Collections.Generic;
using System.Text;

namespace TaskOrganizerLibrary.Model
{
    public class DeleteSelectionModel
    {
        public bool DeleteUnfinished { get; set; } = true;
        public bool DeleteActivated { get; set; } = true;
        public bool DeleteFinished { get; set; } = true;
    }
}
