using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIRH.DTO
{
    [DataContract]
    public class CPorcentajeRentaDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Porcentaje de Renta")]
        public Double? PorcRenta { get; set; }

        [DataMember]
        [DisplayName("Fecha de inicio")]
        public DateTime? FecInicio { get; set; }

        [DataMember]
        [DisplayName("Fecha de final")]
        public DateTime? FecFinal { get; set; }
        [DataMember]
        [DisplayName("Rango inicial")]
        public Double? RangoInicial { get; set; }

        [DataMember]
        [DisplayName("Rango final")]
        public Double? RangoFinal { get; set; }
    }
}
