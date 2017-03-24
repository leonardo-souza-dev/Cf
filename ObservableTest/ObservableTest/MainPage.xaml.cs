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
            App.UsuarioVM.Usuario = new UsuarioModel("Gilberto", "http://www.idosodepirai.com.br/resources/3x4%20gilberto.jpg");

            App.PostVM.CarregarPosts();

            this.BindingContext = App.PostVM;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PerfilView());
        }
    }
}
