using System.Runtime.Serialization;

namespace ObservableTest.Model
{
    [DataContract]
    public class RespostaCurtir
    {
        [DataMember]
        internal string mensagem;

        [DataMember]
        internal RespostaCurtida curtida;

    }

    [DataContract]
    public class RespostaCurtida
    {
        [DataMember]
        internal int postId;

        [DataMember]
        internal int usuarioId;
    }
}
