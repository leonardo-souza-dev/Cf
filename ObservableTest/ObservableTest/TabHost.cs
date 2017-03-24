using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObservableTest
{
    public class TabHost : TabbedPage
    {
        private MainPage Explorar;
        //private UploadView Upload;
        private PerfilView Perfil;

        public TabHost()
        {
            Explorar = new MainPage();
            //Upload = new UploadView(this);
            Perfil = new PerfilView();

            Children.Add(Explorar);
            //Children.Add(Upload);
            Children.Add(Perfil);
        }
    }
}
