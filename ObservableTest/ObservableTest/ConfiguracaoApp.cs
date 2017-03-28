using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ObservableTest
{
    public class ConfiguracaoApp
    {
        bool DebugarAndroid = false;
        public bool UsarCloud { get { return true; } }

        public string ObterUrlAvatar(string nomeArquivo)
        {
            return this.ObterUrlBaseWebApi() + "foto?na=" + nomeArquivo;
        }

        public string ObterUrlBaseWebApi(string metodo = null)
        {

            string enderecoBase = string.Empty;

            if (UsarCloud)
                enderecoBase = "https://cfwebapi.herokuapp.com/";
            else
            {
                enderecoBase += "http://";
                if (DebugarAndroid)
                    enderecoBase += "10.0.2.2";
                else
                    enderecoBase += "localhost";
                enderecoBase += ":8084/";
            }

            enderecoBase += string.IsNullOrEmpty(metodo) ? "api/" : "api/" + metodo;

            return enderecoBase;
        }
    }
}
