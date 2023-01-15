using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIRH.DTO
{
    [DataContract]
    public class CCatalogoRubroDeduccionDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Codigo del Rubro")]
        public string Codigo { get; set; }

        [DataMember]
        [DisplayName("Descripción del Rubro")]
        public string Descripcion { get; set; }
    }
}
