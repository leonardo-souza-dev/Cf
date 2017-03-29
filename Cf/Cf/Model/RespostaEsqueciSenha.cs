using System.Runtime.Serialization;

namespace Cf.Model
{
    [DataContract]
    public class RespostaEsqueciSenha
    {
        [DataMember]
        internal string mensagem;

        [DataMember]
        internal bool sucesso;
    }
}
