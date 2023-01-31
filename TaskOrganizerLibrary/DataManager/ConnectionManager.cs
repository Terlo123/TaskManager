using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizerLibrary.DataManager
{
    public static class ConnectionManager
    {
        private static DataAccess _dataConnection = DataAccess.TextFile;

        private static string _selectedDate;

        public static IDataType GetConnectionLocation()
        {
            if (_dataConnection == DataAccess.TextFile)
            {
                return new TextFileManager();
            }
            else
            {
                return new DataBaseManager();
            }
        }

        public static string FilePathToTextFile()
        {
            string fileName = $"tasks{_selectedDate}.txt";
            string path = Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), fileName);
            return path;
        }

        public static void GetDate(DateTime? date)
        {
            string dateString = date.ToString();
            string[] tempString = dateString.Split(' ');
            string stringToSave = tempString[0].Replace("/", "_");
            _selectedDate = stringToSave;
        }
    }

    public enum DataAccess
    {
        TextFile,
        DataBase
    }
}
