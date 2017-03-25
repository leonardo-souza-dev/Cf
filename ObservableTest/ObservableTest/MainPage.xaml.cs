using ObservableTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObservableTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //MOCK DE DADOS
            // App.UsuarioVM.Usuario.Get(1);

            App.PostVM.CarregarPosts();

            this.BindingContext = App.PostVM;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PerfilView());
        }
    }
}
