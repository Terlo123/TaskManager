using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerAndro.Model;
using TaskOrganizerLibrary.DataManager;
using TaskOrganizerLibrary.Model;
using Xamarin.Forms;

namespace TaskOrganizerAndro.ViewModel
{
    public class DailyViewModel
    {
        public Command<EventsModel> DeleteCommand { get; private set; }
        public Command<EventsModel> SetState { get; private set; }
        public Command<EventsModel> MoveUp { get; private set; }
        public Command<EventsModel> MoveDown { get; private set; }
        
        public DailyViewModel()
        {
            DeleteCommand = new Command<EventsModel>(model =>
            {
                EventList.Remove(model);
            });

            SetState = new Command<EventsModel>(model => 
            {
                if (model.Finished == false && model.Activated == false)
                {
                    model.Finished = false;
                    model.Activated = true;
                }
                else if (model.Activated == true)
                {
                    model.Finished = true;
                    model.Activated = false;
                }
                else if (model.Finished == true)
                {
                    model.Finished = false;
                    model.Activated = false;
                }
            });

            MoveUp = new Command<EventsModel>(model =>
            {
                if (EventList.IndexOf(model) != 0)
                {
                    var index = EventList.IndexOf(model);
                    EventList.Move(index, index - 1);
                }                        
            });

            MoveDown = new Command<EventsModel>(model =>
            {
                if (EventList.Count - 1 != EventList.IndexOf(model))
                {
                    var index = EventList.IndexOf(model);
                    EventList.Move(index, index + 1); 
                }
            });
        }

        public bool IsSaved { get; set; } = true;

        //Expander prop needed for Expander's to start closed.
        public bool ExpanderExpansion { get; set; } = false;

        private ObservableCollection<EventsModel> _eventList = new ObservableCollection<EventsModel>();
        public ObservableCollection<EventsModel> EventList
        {
            get { return _eventList; }
            set { _eventList = value; } 
        }
        
        public void LoadTaskList(DateTime loadDate)
        {
            EventList.Clear();          
            var _saveType = ConnectionManager.GetConnectionLocation();
            ConnectionManager.GetDate(loadDate);

            if (_saveType is TextFileManager)
            {
                List<LibraryEventsModel> tasks = _saveType.LoadFrom();
                foreach (var task in tasks)
                {
                    EventList.Add(new EventsModel
                    {
                        Event = task.Event,
                        Finished = task.Finished,
                        Activated = task.Activated,
                        Description = task.Description
                    });
                }
            }
            
            else if (_saveType is DataBaseManager)
            {
                //_type.LoadFrom();
            }
        }

        public void SaveTaskList(DateTime saveDate)
        {
            var _saveType = ConnectionManager.GetConnectionLocation();
            
            if (_saveType is TextFileManager)
            {
                ConnectionManager.GetDate(saveDate);

                List<LibraryEventsModel> listToSave = new List<LibraryEventsModel>();

                foreach (var item in EventList)
                {
                    LibraryEventsModel libraryModel = new LibraryEventsModel() { 
                        Event = item.Event, 
                        Description = item.Description,
                        Activated = item.Activated,
                        Finished = item.Finished
                    };
                    listToSave.Add(libraryModel);
                }

                _saveType.SaveTo(listToSave);
            }
            else if (_saveType is DataBaseManager)
            {
                //_type.SaveTo();
            }

            IsSaved = true;
        }

        public void AddNewEvent(string task)
        {
            var output = EventList.Any(x => x.Event.Equals(task));
            if (!output)
            {
                EventList.Add(new EventsModel() { Event = task });
            }       
        }

        public DateTime DateTimeForward(DateTime date)
        {
            return date.AddDays(1);
        }

        public DateTime DateTimeBackward(DateTime date)
        {
            return date.AddDays(-1);
        }
        public void CopyTasksList(List<object> taskList,object userInput)
        {
            var _saveType = ConnectionManager.GetConnectionLocation();

            List<LibraryEventsModel> libraryList = new List<LibraryEventsModel>();

            foreach (var item in taskList)
            {
                LibraryEventsModel libraryModel = new LibraryEventsModel()
                {
                    Event = ((EventsModel)item).Event,
                    Description = ((EventsModel)item).Description,
                };
                libraryList.Add(libraryModel);
            }

            if (_saveType is TextFileManager)
            {
                _saveType.PasteTaskList(libraryList, (SaveTemplateModel)userInput);
            }
            else if (_saveType is DataBaseManager)
            {
                //_saveType.PasteTaskList(libraryList, userInput);
            }          
        }
    }
}
