using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace SIRH.DTO
{
    [DataContract]
    public class CClaseDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Clase")]
        public string DesClase { get; set; }
        [DataMember]
        [DisplayName("Estado")]
        public int IndEstadoClase { get; set; }
        [DataMember]
        [DisplayName("Categoria")]
        public int IndCategoria { get; set; }
    }
}
