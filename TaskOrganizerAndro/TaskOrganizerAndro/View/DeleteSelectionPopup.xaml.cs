using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskOrganizerLibrary.Model;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskOrganizerAndro.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteSelectionPopup : Popup
    {
        private DeleteSelectionModel _deleteModel = new DeleteSelectionModel();
        public DeleteSelectionPopup()
        {
            InitializeComponent();
            BindingContext = _deleteModel;
        }

        private void Button_ClickedDelete(object sender, EventArgs e)
        {
            Dismiss(_deleteModel);
        }

        private void Button_ClickedCancel(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}