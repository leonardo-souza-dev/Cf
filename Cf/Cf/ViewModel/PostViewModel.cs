using Cf.Data;
using Cf.Model;
using Cf.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cf.ViewModel
{
    public class PostViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PostModel> Posts { get { return _posts; } set { _posts = value; OnPropertyChanged("Posts"); } }
        private ObservableCollection<PostModel> _posts = new ObservableCollection<PostModel>();

        public async Task<RespostaStatus> Salvar(PostModel post)
        {
            try
            {
                var nomeFotoUploadada = PostRepository.UploadFoto(post.ObterByteArrayFoto(),
                                                                      post.Usuario.ID.ToString().PadLeft(6, '0') + ".jpg",
                                                                      post.Usuario.ID.ToString()).Result;
                post.NomeArquivo = nomeFotoUploadada.nomeArquivo;

                var postSalvo = await PostRepository.SalvarPost(post);
                postSalvo.Usuario = post.Usuario;
                InserirPost(postSalvo);

                return RespostaStatus.Sucesso;
            }
            catch (System.Exception)
            {
                return RespostaStatus.ErroGenerico;
            }
        }

        public void InserirPost(PostModel post)
        {
            Posts.Insert(0, post);
        }

        public async void CarregarPosts()
        {
            var listaPosts = new List<PostModel>();

            listaPosts = await PostRepository.ObterPosts();

            for (int index = 0; index < listaPosts.Count; index++)
            {
                var post = listaPosts[index];

                AssociaUsuarioLogado(post);

                if (index + 1 > Posts.Count || Posts[index].Equals(post))
                {
                    foreach (var curtida in post.Curtidas)
                    {
                        if (curtida.UsuarioId == App.UsuarioVM.Usuario.ID)
                        {
                            post.CurtidaHabilitada = false;
                        }
                    }
                    Posts.Insert(index, post);
                }
            }
        }

        private static void AssociaUsuarioLogado(PostModel post)
        {
            if (App.UsuarioVM.Usuario.ID == post.Usuario.ID)
            {
                post.Usuario = App.UsuarioVM.Usuario;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }
}
