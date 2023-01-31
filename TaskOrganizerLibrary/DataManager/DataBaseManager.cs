using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerLibrary.Model;

namespace TaskOrganizerLibrary.DataManager
{
    public class DataBaseManager : IDataType
    {
        public List<LibraryEventsModel> LoadFrom()
        {
            throw new NotImplementedException();
        }

        public void PasteTaskList(List<LibraryEventsModel> listToSave, SaveTemplateModel userInput)
        {
            throw new NotImplementedException();
        }

        public void SaveTo(List<LibraryEventsModel> listToSave)
        {
            throw new NotImplementedException();
        }

        public void ClearSelectedTasks(DateTime resetMonth, object model)
        {
            throw new NotImplementedException();
        }
    }
}
