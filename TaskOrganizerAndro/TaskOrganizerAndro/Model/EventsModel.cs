using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaskOrganizerAndro.Model
{
    public class EventsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Event { get; set; } = "";

        public string Description { get; set; } = "";

        private bool _activated;

        public bool Activated
        {
            get { return _activated; }
            set 
            { 
                _activated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Activated"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SwipeColor"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusText"));
            }
        }

        private bool _finished;

        public bool Finished
        {
            get { return _finished; }
            set 
            { 
                _finished = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Finished"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SwipeColor"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusText"));
            }
        }

        private ImageSource _icon;

        public ImageSource Icon
        {
            get 
            {
                if (Finished)
                {
                    return "vmark.png";
                }
                else if (Activated)
                {
                    return "imark.png";
                }
                else
                {
                    return "xmark.png";
                }
            
            }
            set 
            { 
                _icon = value;
            }
        }
        private string _statusText;

        public string StatusText
        {
            get 
            {
                if (Finished)
                {
                    return "Resetuj";
                }
                else if (Activated)
                {
                    return "Zakończ";
                }
                else
                {
                    return "Aktywuj";
                }
            }
            set
            {
                _statusText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusText"));
            }
        }

        private Color _swipeColor;

        public Color SwipeColor
        {
            get
            {
                if (Finished)
                {
                    return Color.Orange;
                }
                else if (Activated)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.Yellow;
                }
            }
            set
            {
                _swipeColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SwipeColor"));
            }
        }
        private Color _statusColor;

        public Color StatusColor
        {
            get
            {
                if (Finished)
                {
                    return Color.Green;
                }
                else if (Activated)
                {
                    return Color.Yellow;
                }
                else
                {
                    return Color.Red;
                }
            }
            set
            {
                _statusColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StatusColor"));
            }
        }
    }
}

