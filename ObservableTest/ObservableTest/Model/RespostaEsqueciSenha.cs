using System.Runtime.Serialization;

namespace ObservableTest.Model
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
