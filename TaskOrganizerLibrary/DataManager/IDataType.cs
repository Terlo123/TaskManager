using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerLibrary.Model;

namespace TaskOrganizerLibrary.DataManager
{
    public interface IDataType
    {
        void SaveTo(List<LibraryEventsModel> listToSave);
        List<LibraryEventsModel> LoadFrom();
        void PasteTaskList(List<LibraryEventsModel> listToSave, SaveTemplateModel userSaveTemplate);
        void ClearSelectedTasks(DateTime resetMonth, object model);
    }
}
