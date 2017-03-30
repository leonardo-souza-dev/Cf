using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cf.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Cf.Data
{
    public static class UsuarioRepository
    {
        public static async Task<bool> TesteConexao()
        {
            try
            {
                var response = await new HttpClient().GetAsync(App.Config.ObterUrlBaseWebApi() + "fetch");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<RespostaUploadAvatar> UploadAvatar(byte[] imagem, string sufixoAvatar, string sufixoUsuarioId)
        {
            var urlUpload = App.Config.ObterUrlBaseWebApi("uploadavatar");
            var requestContent = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(imagem);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "av", sufixoAvatar);
            requestContent.Add(new StringContent(sufixoUsuarioId), "usuarioId");

            var client = new HttpClient();
            var response = client.PostAsync(urlUpload, requestContent).Result;//upload da foto do usuario
            var stream = await response.Content.ReadAsStreamAsync();
            var ser = new DataContractJsonSerializer(typeof(RespostaUploadAvatar));
            stream.Position = 0;

            var respostaUpload = (RespostaUploadAvatar)ser.ReadObject(stream);//retornando nome do arquivo uploadado

            return respostaUpload;
        }

        public static async Task<UsuarioModel> Atualizar()
        {
            var request = new
            {
                nomeUsuario = App.UsuarioVM.Usuario.Nome,
                usuarioId = App.UsuarioVM.Usuario.ID,
                email = App.UsuarioVM.Usuario.Email,
                nomeArquivoAvatar = App.UsuarioVM.Usuario.NomeArquivoAvatar
            };
            var resposta = await Resposta<UsuarioModel>(request, "atualizarusuario");

            return resposta;
        }

        public static async Task<RespostaCadastro> Cadastro(string emailDigitado, string senhaDigitada, string nomeUsuarioDigitado)
        {
            var resposta = await Resposta<RespostaCadastro>(new { email = emailDigitado, senha = senhaDigitada, nomeUsuario = nomeUsuarioDigitado }, "cadastro");

            return resposta;
        }

        public static async Task<UsuarioModel> Login(string emailDigitado, string senhaDigitada)
        {
            var resposta = await Resposta<UsuarioModel>(new { email = emailDigitado, senha = senhaDigitada }, "login");

            return resposta;
        }

        public static async Task<RespostaEsqueciSenha> EsqueciSenha(string emailDigitado)
        {
            var resposta = await Resposta<RespostaEsqueciSenha>(new { email = emailDigitado }, "esquecisenha");

            return resposta;
        }

        private static async Task<T> Resposta<T>(object conteudo, string metodo, bool ehDownload = false)
        {
            try
            {
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(conteudo);
                var contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(UrlBase(metodo), contentPost);
                var r = Objetos(response);
                var stream = await response.Content.ReadAsStreamAsync();
                var ser = new DataContractJsonSerializer(typeof(T));
                stream.Position = 0;
                T t = (T)ser.ReadObject(stream);

                var t2 = Objetos(t);
                return t;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static object Objetos(object objectos)
        {
            return objectos;
        }

        private static string UrlBase(string metodo)
        {
            return App.Config.ObterUrlBaseWebApi(metodo);
        }

    }
}
