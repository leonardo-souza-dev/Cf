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
using System.Diagnostics;

namespace Cf.Data
{
    public static class PostRepository
    {
        public static async Task<List<PostModel>> ObterPosts()
        {
            var respostaPosts = await Resposta<List<PostModel>>(null, "obterposts");

            Debug.WriteLine("<DEBUG>");
            foreach (var item in respostaPosts)
            {
                Debug.WriteLine(item.ID);
                Debug.WriteLine(item.Usuario.ID);
                Debug.WriteLine(item.Usuario.AvatarUrl);
            }
            Debug.WriteLine("</DEBUG>");
            return respostaPosts;
        }

        public static async Task<RespostaCurtir> Descurtir(int usuarioIdPassado, int postIdPassado)
        {
            var resposta = await Resposta<RespostaCurtir>(new { usuarioId = usuarioIdPassado, postId = postIdPassado }, "descurtir");

            return resposta;
        }

        public static async Task<RespostaCurtir> Curtir(int usuarioIdPassado, int postIdPassado)
        {
            var resposta = await Resposta<RespostaCurtir>(new { usuarioId = usuarioIdPassado, postId = postIdPassado }, "curtir");

            return resposta;
        }

        private static async Task<T> Resposta<T>(object conteudo, string metodo, bool ehDownload = false)
        {
            var httpClient = new HttpClient();
            var uri = App.Config.ObterUrlBaseWebApi(metodo);

            if (conteudo != null)
            {
                var retorno = await new ClienteHttp().PostAsync<T>(uri, conteudo);

                return retorno;
            }
            else
            {
                var retorno = await new ClienteHttp().GetAsync<T>(uri);

                return retorno;
            }
        }

        public static async Task<PostModel> SalvarPost(PostModel post)
        {
            var httpRequest = new HttpClient();
            string userJson = JsonConvert.SerializeObject(post);
            var response = await httpRequest.PostAsync(App.Config.ObterUrlBaseWebApi("salvarpost"),
                new StringContent(userJson, Encoding.UTF8, "application/json"));

            var streamm = await response.Content.ReadAsStreamAsync();
            var ser = new DataContractJsonSerializer(typeof(PostModel));
            streamm.Position = 0;
            var respostaUpload = (PostModel)ser.ReadObject(streamm);

            var r = (PostModel)Objetos(respostaUpload);

            return r;
        }

        public static async Task<RespostaUploadAvatar> UploadFoto(byte[] imagem, string sufixoImagem, string sufixoUsuarioId)
        {
            var urlUpload = App.Config.ObterUrlBaseWebApi("uploadfoto"); Objetos(urlUpload);/////////////
            var requestContent = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(imagem);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            requestContent.Add(imageContent, "cf", sufixoImagem);
            requestContent.Add(new StringContent(sufixoUsuarioId), "usuarioId");
            Objetos(requestContent);/////////////////
            var client = new HttpClient();
            var response2 = await client.PostAsync(urlUpload, requestContent);
            var stream = await response2.Content.ReadAsStreamAsync();
            var ser = new DataContractJsonSerializer(typeof(RespostaUploadAvatar));
            stream.Position = 0;
            var resposta = (RespostaUploadAvatar)ser.ReadObject(stream);

            return resposta;
        }

        public static object Objetos(object objectos)
        {
            return objectos;
        }
        private static PostModel Convert(System.IO.Stream stream)
        {
            var ser3 = new DataContractJsonSerializer(typeof(PostModel));
            stream.Position = 0;
            PostModel resposta3 = (PostModel)ser3.ReadObject(stream);
            Debug.WriteLine("PostModel");
            Debug.WriteLine(resposta3.ID);
            Debug.WriteLine(resposta3.Legenda);
            return resposta3;
        }
    }
}
