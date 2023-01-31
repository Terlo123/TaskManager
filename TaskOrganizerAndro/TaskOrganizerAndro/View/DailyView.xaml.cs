using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerAndro.Helpers;
using TaskOrganizerAndro.View;
using TaskOrganizerAndro.ViewModel;
using TaskOrganizerLibrary.DataManager;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskOrganizerAndro
{
    public partial class DailyView : ContentPage
    {
        private DailyViewModel _model = new DailyViewModel();

        public DailyView()
        {
            InitializeComponent();
            BindingContext = _model;
            ConnectionManager.GetDate(MainCalendar.Date);
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            MainCalendar.Date = Connector.DateToPass;
            _model.LoadTaskList(Connector.DateToPass);
        }

        private async void MainCalendar_DateSelected(object sender, DateChangedEventArgs e)
        {          
            try
            {
                if (_model.IsSaved == false)
                {
                    var userOutput = await DisplayAlert(
                        "Wprowadzono zmiany",
                        "Zmiany nie zostały zapisane czy chcesz je zapisac?",
                        "Zapisz",
                        "Nie Zapisuj");

                    if (userOutput)
                    {
                        _model.SaveTaskList(MainCalendar.Date);
                        _model.LoadTaskList(MainCalendar.Date);
                        EventListView.SelectedItems.Clear();
                    }
                    else if (!userOutput)
                    {
                        _model.IsSaved = true;
                    }
                }
                else
                {
                    _model.LoadTaskList(MainCalendar.Date);
                    EventListView.SelectedItems.Clear();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error" ,$"{ex}", "OK");
                throw;
            }
        }

        private void AddEventClick(object sender, EventArgs e)
        {
            try
            {
                if (MainCalendar.Date != null)
                {
                    if (EventAddTextBox.Text != "" && EventAddTextBox.Text != null)
                    {
                        _model.AddNewEvent(EventAddTextBox.Text);
                        EventAddTextBox.Text = string.Empty;
                        _model.IsSaved = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"{ex}", "OK");
                throw;
            }
        }

        private void SaveList_Click(object sender, EventArgs e)
        {
            try
            {
                if (MainCalendar.Date != null)
                {
                    _model.SaveTaskList(MainCalendar.Date);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"{ex}", "OK");
                throw;
            }
        }

        private async void ResetDate_Clicked(object sender, EventArgs e)
        {
            if (_model.IsSaved == false)
            {              
                var userOutput = await DisplayAlert(
                    "Wprowadzono zmiany",
                    "Zmiany nie zostały zapisane czy chcesz je zapisac?",
                    "Zapisz",
                    "Nie Zapisuj");

                if (userOutput)
                {
                    _model.SaveTaskList(MainCalendar.Date);
                    MainCalendar.Date = DateTime.Now;
                    EventListView.SelectedItems.Clear();
                }
                else if (!userOutput)
                {
                    _model.IsSaved = true;
                }
            }
            else
            {
                MainCalendar.Date = DateTime.Now;
                EventListView.SelectedItems.Clear();              
            }
        }

        private async void DateBackward_Clicked(object sender, EventArgs e)
        {
            if (_model.IsSaved == false)
            {
                var userOutput = await DisplayAlert(
                    "Wprowadzono zmiany", 
                    "Zmiany nie zostały zapisane czy chcesz je zapisac?", 
                    "Zapisz", 
                    "Nie Zapisuj");

                if (userOutput)
                {
                    _model.SaveTaskList(MainCalendar.Date);
                    MainCalendar.Date = _model.DateTimeBackward(MainCalendar.Date);
                    EventListView.SelectedItems.Clear();
                }
                else if (!userOutput)
                {
                    _model.IsSaved = true;
                }
            }
            else
            {
                MainCalendar.Date = _model.DateTimeBackward(MainCalendar.Date);
                EventListView.SelectedItems.Clear();
            }
        }

        private async void DateForward_Clicked(object sender, EventArgs e)
        {
            if (_model.IsSaved == false)
            {
                var userOutput = await DisplayAlert(
                    "Wprowadzono zmiany", 
                    "Zmiany nie zostały zapisane czy chcesz je zapisac?",
                    "Zapisz", 
                    "Nie Zapisuj");

                if (userOutput)
                {
                    _model.SaveTaskList(MainCalendar.Date);
                    MainCalendar.Date = _model.DateTimeForward(MainCalendar.Date);
                    EventListView.SelectedItems.Clear();
                }
                else if (!userOutput)
                {
                    _model.IsSaved = true;
                }
            }
            else
            {
                MainCalendar.Date = _model.DateTimeForward(MainCalendar.Date);
                EventListView.SelectedItems.Clear();
            }
        }

        private async void CopyTask_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (EventListView.SelectedItems.Count != 0)
                {
                    var result = await Navigation.ShowPopupAsync(new SaveListPopup(MainCalendar.Date));
                    if (result == null)
                    {
                        EventListView.SelectedItems.Clear();
                    }
                    else
                    {
                        _model.SaveTaskList(MainCalendar.Date);
                        _model.CopyTasksList(EventListView.SelectedItems.ToList(), result);
                        EventListView.SelectedItems.Clear();
                    }
                }
                else
                {
                    await DisplayAlert("Kopiowanie", "Nie wybrano zadnego zadania z list", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex}", "OK");
                throw;
            }
        }

        private void Editor_Completed(object sender, EventArgs e)
        {
            _model.IsSaved = false;
        }

        private void SwipeChangeStatus_Invoked(object sender, EventArgs e)
        {
            _model.IsSaved = false;
        }

        private void SwipeDeleteItem_Invoked(object sender, EventArgs e)
        {
            _model.IsSaved = false;
            EventListView.SelectedItems.Remove(((SwipeItem)sender).CommandParameter);           
        }

        private async void ClearList_Clicked(object sender, EventArgs e)
        {
            var output = await DisplayAlert("Usuwanie", "Czy chcesz usunac wszystkie zdania?", "Tak", "Nie");
            if (output)
            {
                _model.IsSaved = false;
                _model.EventList.Clear(); 
            }
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            if (_model.IsSaved == false)
            {
                var userOutput = await DisplayAlert(
                    "Wprowadzono zmiany",
                    "Zmiany nie zostały zapisane czy chcesz je zapisac?",
                    "Zapisz",
                    "Nie Zapisuj");

                if (userOutput)
                {
                    _model.SaveTaskList(MainCalendar.Date);
                    EventListView.SelectedItems.Clear();
                    await Navigation.PushAsync(new MonthlyView(MainCalendar.Date));
                    
                }
                else if (!userOutput)
                {
                    _model.IsSaved = true;
                    await Navigation.PushAsync(new MonthlyView(MainCalendar.Date));
                }
            }
            else
            {
                EventListView.SelectedItems.Clear();
                await Navigation.PushAsync(new MonthlyView(MainCalendar.Date));              
            }
        }
    }   
}
