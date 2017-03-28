using ObservableTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableTest.ViewModel
{
    public class PostViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PostModel> _posts = new ObservableCollection<PostModel>();
        public ObservableCollection<PostModel> Posts
        {
            get
            {
                return _posts;
            }
            set
            {
                _posts = value;
                OnPropertyChanged("Posts");
            }
        }

        //MOCK DE DADOS
        public void CarregarPosts()
        {
            ////////////if (App.UsuarioVM.Usuario == null)
            ////////////{
            ////////////    App.UsuarioVM.Usuario = App.Database.GetUsuarioAsync().Result.Where(u=>u.Nome == "Gilberto").FirstOrDefault();
            ////////////}
            ////////////if (App.UsuarioVM.Usuario == null)
            ////////////{ 
            ////////////    int idGilbertoMock = App.Database.SaveItemAsync(new UsuarioModel() { Nome = "Gilberto"}).Result;
            ////////////    App.UsuarioVM.Usuario = App.Database.GetItemAsync(idGilbertoMock).Result;
            ////////////}
            ////////////App.UsuarioVM.Usuario.AvatarUrl = "https://cvtrampos.files.wordpress.com/2013/05/ft-34.jpg";
            ////////////App.UsuarioVM.Usuario.Email = "sukinho@dog.com.br";

            if (App.UsuarioVM.Usuario == null)
            {
                App.UsuarioVM.Usuario = App.UsuarioVM.Login("qwe", "qwe").Result;
            }

            var post1 = new PostModel()
            {
                Legenda = "Esse é o cartão Sênior",
                Usuario = App.UsuarioVM.Usuario,
                FotoUrl = "http://www.blogcartaobom.com.br/wp-content/uploads/2015/01/senior-frente.png"
            };

            Posts.Add(post1);

            //Posts.Add(new PostModel()
            //{
            //    Legenda = "Com eses cartão eiu pago meia",
            //    Usuario = new UsuarioModel(2, "Joana D'Arc", "https://cvtrampos.files.wordpress.com/2013/05/ft-34.jpg"),
            //    FotoUrl = "http://essaseoutras.xpg.uol.com.br/wp-content/uploads/2011/01/como-fazer-cart%C3%A3o-bom-escolar.jpg"
            //});
            //Posts.Add(new PostModel()
            //{
            //    Legenda = "Onde trabalho",
            //    Usuario = App.UsuarioVM.Usuario,
            //    FotoUrl = "https://yt3.ggpht.com/--j89bU5NQj8/AAAAAAAAAAI/AAAAAAAAAAA/fSjK-D6_uUY/s900-c-k-no-mo-rj-c0xffffff/photo.jpg"
            //});
            //Posts.Add(new PostModel()
            //{
            //    Legenda = "Meu cartão chegou",
            //    Usuario = new UsuarioModel(3, "Pessoa", "http://blog-fipecafi.imprensa.ws/wp-content/uploads/2012/01/Alberto-3x4.jpg"),
            //    FotoUrl = "http://1.bp.blogspot.com/-suxwukgsRiM/Ttbu4DoxbII/AAAAAAAANy0/HucMMhoozV8/s1600/bomCOMUM.jpg"
            //});

            //DebugHelper debugHelper = new DebugHelper();
            //debugHelper.Print(Posts);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }
}
