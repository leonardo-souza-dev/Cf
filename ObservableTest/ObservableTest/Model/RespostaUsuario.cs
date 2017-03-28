using System.Runtime.Serialization;

namespace ObservableTest.Model
{
    [DataContract]
    public class RespostaUsuario
    {
        [DataMember]
        internal int usuarioId;

        [DataMember]
        internal string email;

        [DataMember]
        internal string nomeArquivoAvatar;

        [DataMember]
        internal string nomeUsuario;
    }
}
