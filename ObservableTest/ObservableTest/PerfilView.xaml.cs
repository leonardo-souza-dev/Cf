using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ObservableTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilView : ContentPage
    {
        MediaFile File;
        bool modoEdicao = true;
        Stream Stream = null;

        public PerfilView()
        {
            InitializeComponent();

            this.BindingContext = App.UsuarioVM.Usuario;
        }

        protected void Salvar_Clicked(object sender, EventArgs e)
        {
            //chamada do metodo que persiste
        }

        protected async void OnAvatarImageTapped(object sender, EventArgs e)
        {
            if (modoEdicao)
            {
                //var cameraNaoDisponivel = !CrossMedia.Current.IsCameraAvailable;
                var escolherFotoNaoSuportado = !CrossMedia.Current.IsPickPhotoSupported;

                if (/*cameraNaoDisponivel || */escolherFotoNaoSuportado)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                File = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { CompressionQuality = 10 });

                if (File == null)
                    return;

                App.UsuarioVM.EditouAvatar = true;

                avatarImage.Source = ImageSource.FromStream(() =>
                {
                    Stream stream = File.GetStream();
                    Stream = File.GetStream();
                    File.Dispose();

                    return stream;
                });
            }
        }
    }
}
