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
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public UsuarioViewModel() { }


        #region Propriedades

        public string TempEmail;
        public string TempNomeArquivoAvatar;
        public string TempNomeUsuario;

        public int Id { get; set; }
        public string Nome { get; set; }
        public object AvatarUrl { get; set; }
        public bool EditouAvatar { get; set; }

        private UsuarioModel usuario;
        public UsuarioModel Usuario
        {
            get
            {
                return usuario;
            }
            set
            {
                usuario = value;
            }
        }

        #endregion

        public async Task<RespostaStatus> AtualizarCadastro(byte[] bytes)
        {
            try
            {
                bool mudou = Usuario.Email != TempEmail || Usuario.Nome != TempNomeUsuario || Usuario.AvatarUrl != TempNomeArquivoAvatar || EditouAvatar;

                if (mudou)
                {
                    if (EditouAvatar)
                    {
                        //var nomeArquivo = App.UsuarioVM.UploadAvatar(bytes).Result;
                        var nomeArquivo = "https://studiosol-a.akamaihd.net/letras/50x50/fotos/6/5/4/7/6547bd58e87551e86aaa494f8ba21d9f-tb2.jpg";

                        App.UsuarioVM.Usuario.AvatarUrl = nomeArquivo;
                    }

                    try
                    {
                        //var usuarioAtualizado = await UsuarioRepository.Atualizar();
                        UsuarioModel usuarioAtualizado = new UsuarioModel() { ID = 1 };//MOCK

                        if (usuarioAtualizado.ID == -1 && !EditouAvatar)
                        {
                            return RespostaStatus.JaExiste;
                        }
                    }
                    catch (Exception ex)
                    {
                        return RespostaStatus.ErroGenerico;
                    }
                }

                return RespostaStatus.Sucesso;
            }
            catch (Exception ex)
            {
                return RespostaStatus.ErroGenerico;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }
}
