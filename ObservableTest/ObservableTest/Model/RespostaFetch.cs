using System.Runtime.Serialization;

namespace ObservableTest.Model
{
    [DataContract]
    public class RespostaFetch
    {
        [DataMember]
        internal int status;
    }
}
