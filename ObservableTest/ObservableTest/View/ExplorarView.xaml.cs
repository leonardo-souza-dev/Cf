using ObservableTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObservableTest.View
{
    public partial class ExplorarView : ContentPage
    {
        public ExplorarView()
        {
            InitializeComponent();

            App.PostVM.CarregarPosts();

            this.BindingContext = App.PostVM;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PerfilView());
        }
    }
}
