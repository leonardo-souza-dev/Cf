using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ObservableTest.Model
{
    [DataContract]
    public class CurtidaModel
    {
        [DataMember(Name = "postId")]
        public int PostId { get; set; }

        [DataMember(Name = "usuarioId")]
        public int UsuarioId { get; set; }
    }
}
