using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskOrganizerAndro.Helpers;
using TaskOrganizerAndro.Model;
using TaskOrganizerAndro.ViewModel;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace TaskOrganizerAndro.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthlyView : ContentPage
    {        
        private MonthlyViewModel _model = new MonthlyViewModel();
        public MonthlyView(DateTime startDate)
        {
            InitializeComponent();
            BindingContext = _model;
            MonthlyDatePicker.Date = startDate;
            _model.LoadMonthlyTaskList(startDate);
        }

        private void MonthlyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            _model.LoadMonthlyTaskList(MonthlyDatePicker.Date);
        }

        private async void ResetMonth_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await Navigation.ShowPopupAsync(new DeleteSelectionPopup());

                if (result != null)
                {
                    _model.DeleteSelectedTypeOfTasks(MonthlyDatePicker.Date, result);
                    _model.LoadMonthlyTaskList(MonthlyDatePicker.Date);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex}", "OK");
                throw;
            }
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Button_ClickedReturnToDailyView(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}