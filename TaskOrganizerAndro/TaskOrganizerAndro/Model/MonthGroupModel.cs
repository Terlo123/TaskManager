using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace TaskOrganizerAndro.Model
{
    public class MonthGroupModel : List<EventsModel>
    {
        public string GroupTitle { get; set; }
        public DateTime DateMarking { get; set; }


        public MonthGroupModel(string groupTitle, List<EventsModel> model, DateTime dateMarking) : base(model)
        {
            GroupTitle = groupTitle;
            DateMarking = dateMarking;
        }
    }
}
