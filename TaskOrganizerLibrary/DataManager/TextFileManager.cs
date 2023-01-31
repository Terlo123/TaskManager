using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerLibrary.Model;

namespace TaskOrganizerLibrary.DataManager
{
    public class TextFileManager : IDataType
    {
        public void SaveTo(List<LibraryEventsModel> listToSave)
        {
            string tempText = "";

            foreach (var task in listToSave)
            {
                var tempDesc = task.Description.Replace("\n", " ");
                tempText += $"{task.Event}|{task.Finished}|{task.Activated}|{tempDesc}\n";
            }

            File.WriteAllText(ConnectionManager.FilePathToTextFile(), tempText);
        }

        public List<LibraryEventsModel> LoadFrom()
        {
            if (File.Exists(ConnectionManager.FilePathToTextFile()))
            {
                string[] tasks = File.ReadAllLines(ConnectionManager.FilePathToTextFile());
                List<LibraryEventsModel> tempList = new List<LibraryEventsModel>();
                foreach (var task in tasks)
                {
                    var item = task.Split('|');
                    tempList.Add(new LibraryEventsModel
                    {
                        Event = item[0],
                        Finished = bool.Parse(item[1]),
                        Activated = bool.Parse(item[2]),
                        Description = item[3]
                    });
                }

                return tempList;
            }
            else
            {
                return new List<LibraryEventsModel>();
            }
        }

        public void PasteTaskList(List<LibraryEventsModel> listToSave, SaveTemplateModel userSaveTemplate)
        {
            var saveTemplate = (SaveTemplateModel)userSaveTemplate;
            var numberOfDays = (saveTemplate.EndDate - saveTemplate.StartDate).Days;

            int temp = 0;

            while (temp <= numberOfDays)
            {
                var tempDate = saveTemplate.StartDate.AddDays(temp);
                ConnectionManager.GetDate(tempDate);
                if (saveTemplate.Monday == false && tempDate.DayOfWeek == DayOfWeek.Monday)
                {
                    temp++;
                    continue;
                }
                if (saveTemplate.Tuesday == false && tempDate.DayOfWeek == DayOfWeek.Tuesday)
                {
                    temp++;
                    continue;
                }
                if (saveTemplate.Wednesday == false && tempDate.DayOfWeek == DayOfWeek.Wednesday)
                {
                    temp++;
                    continue;
                }
                if (saveTemplate.Thursday == false && tempDate.DayOfWeek == DayOfWeek.Thursday)
                {
                    temp++;
                    continue;
                }
                if (saveTemplate.Friday == false && tempDate.DayOfWeek == DayOfWeek.Friday)
                {
                    temp++;
                    continue;
                }
                if (saveTemplate.Saturday == false && tempDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    temp++;
                    continue;
                }
                if (saveTemplate.Sunday == false && tempDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    temp++;
                    continue;
                }

                List<LibraryEventsModel> tempList = LoadFrom();

                if (tempList.Count == 0)
                {
                    tempList.AddRange(listToSave);
                }
                else
                {
                    foreach (var task in listToSave)
                    {
                        var output = tempList.Any(x => x.Event.Equals(task.Event));
                        if (!output)
                        {
                            tempList.Add(task);
                        }
                    }
                }

                SaveTo(tempList);
                temp++;
            }
        }
        
        public void ClearSelectedTasks(DateTime resetMonth, object model)
        {
            DateTime resetDate = new DateTime(resetMonth.Year, resetMonth.Month, 1);

            var deleteModel = model as DeleteSelectionModel;

            int temp = 0;

            while (temp < DateTime.DaysInMonth(resetMonth.Year, resetMonth.Month))
            {
                ConnectionManager.GetDate(resetDate.AddDays(temp));
                var list = LoadFrom();

                if (list.Count == 0)
                {
                    temp++;
                    continue;
                }
                else if (deleteModel.DeleteUnfinished && deleteModel.DeleteFinished && deleteModel.DeleteActivated)
                {
                    List<LibraryEventsModel> emptyList = new List<LibraryEventsModel>();
                    SaveTo(emptyList);
                    temp++;
                    continue;
                }
                else
                {
                    if (deleteModel.DeleteUnfinished)
                    {
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            if (!list[i].Activated && !list[i].Finished)
                            {
                                list.RemoveAt(i);
                            }
                        }
                    }
                    if (deleteModel.DeleteActivated)
                    {
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            if (list[i].Activated)
                            {
                                list.RemoveAt(i);
                            }
                        }
                    }
                    if (deleteModel.DeleteFinished)
                    {
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            if (list[i].Finished)
                            {
                                list.RemoveAt(i);
                            }
                        }
                    }
                }

                SaveTo(list);
                temp++;
            }
        }
    }
}
