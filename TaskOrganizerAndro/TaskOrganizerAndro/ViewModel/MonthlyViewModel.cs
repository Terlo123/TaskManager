using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using TaskOrganizerAndro.Helpers;
using TaskOrganizerAndro.Model;
using TaskOrganizerLibrary.DataManager;
using TaskOrganizerLibrary.Model;
using Xamarin.Forms;

namespace TaskOrganizerAndro.ViewModel
{
    public class MonthlyViewModel
    {
        public Command<MonthGroupModel> SetReturnDate { get; private set; }
        public MonthlyViewModel()
        {
            SetReturnDate = new Command<MonthGroupModel>(model =>
            {
                Connector.DateToPass = model.DateMarking;
            });
        }
        
        public ObservableCollection<MonthGroupModel> MonthlyGroupList { get; set; } = new ObservableCollection<MonthGroupModel>();

        public void LoadMonthlyTaskList(DateTime loadMonth)
        {
            MonthlyGroupList.Clear();

            DateTime loadDate = new DateTime(loadMonth.Year, loadMonth.Month, 1);

            var _saveType = ConnectionManager.GetConnectionLocation();

            if (_saveType is TextFileManager)
            {
                int temp = 0;

                var culture = new System.Globalization.CultureInfo("pl-PL");

                while (temp < DateTime.DaysInMonth(loadMonth.Year,loadMonth.Month))
                {
                    ConnectionManager.GetDate(loadDate.AddDays(temp));

                    List<LibraryEventsModel> tasks = _saveType.LoadFrom();
                    List<EventsModel> tempList = new List<EventsModel>();

                    foreach (var task in tasks)
                    {
                        tempList.Add(new EventsModel
                        {
                            Event = task.Event,
                            Finished = task.Finished,
                            Activated = task.Activated,
                            Description = task.Description,
                        });
                    }

                    MonthlyGroupList.Add(new MonthGroupModel
                        ($"{temp + 1}.{loadMonth.Month}.{loadMonth.Year}" +
                        $" {culture.DateTimeFormat.GetDayName(new DateTime(loadMonth.Year, loadMonth.Month, temp + 1).DayOfWeek)}",
                        tempList, new DateTime(loadMonth.Year,loadMonth.Month,temp + 1)));
                    temp++;                 
                }
            }
            else if (_saveType is DataBaseManager)
            {
                //_saveType.LoadFrom();
            }
        }
        public void DeleteSelectedTypeOfTasks(DateTime resetMonth, object deleteModel)
        {
            var _saveType = ConnectionManager.GetConnectionLocation();

            if (_saveType is TextFileManager)
            {
                _saveType.ClearSelectedTasks(resetMonth, deleteModel);
            }
            else if(_saveType is DataBaseManager)
            {
                //_saveType.ClearSelectedTasks();
            }
        }
    }
}
