using Cf.Data;
using Cf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cf.ViewModel
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
                bool mudou = Usuario.Email != TempEmail || Usuario.Nome != TempNomeUsuario || Usuario.NomeArquivoAvatar != TempNomeArquivoAvatar || EditouAvatar;

                if (mudou)
                {
                    if (EditouAvatar)
                    {
                        var arquivoImagemAvatar = App.UsuarioVM.UploadAvatar(bytes).Result;

                        App.UsuarioVM.Usuario.NomeArquivoAvatar = arquivoImagemAvatar;
                    }

                    try
                    {
                        var usuarioAtualizado = await UsuarioRepository.Atualizar();
                        //UsuarioModel usuarioAtualizado = new UsuarioModel() { ID = 1 };//MOCK

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

        public async Task<UsuarioModel> Login(string email, string senha)
        {
            this.Usuario = await UsuarioRepository.Login(email, senha);

            return this.Usuario;
        }

        public async Task<string> UploadAvatar(byte[] bytes)
        {
            var resposta = await UsuarioRepository.UploadAvatar(bytes);
            return resposta.nomeArquivo;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }
    }
}
