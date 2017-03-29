using ObservableTest.View;
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
        private ExplorarView Explorar;
        private UploadView Upload;
        private PerfilView Perfil;

        public TabHost()
        {
            Explorar = new ExplorarView();
            Upload = new UploadView(this);
            Perfil = new PerfilView();

            Children.Add(Explorar);
            Children.Add(Upload);
            Children.Add(Perfil);
        }

        protected override void OnCurrentPageChanged()
        {
            if (this.CurrentPage.Title.Equals("enviar catioro fofo"))
            {
                Upload.EscolherFoto();
            }
        }
    }
}
