using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerAndro.Model;
using TaskOrganizerLibrary.Model;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskOrganizerAndro.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveListPopup : Popup
    {
        private SaveTemplateModel _saveModel = new SaveTemplateModel();
        public SaveListPopup(DateTime time)
        {
            InitializeComponent();
            BindingContext= _saveModel;
            StartDatePicker.Date = time.AddDays(1);
            EndDatePicker.Date = time.AddDays(2);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            Dismiss(_saveModel);
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}