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
    public class CPorcentajeSalarioEscolarDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Número de Resolución")]
        public String NumResolucion { get; set; }

        [DataMember]
        [DisplayName("Porcentaje de Salario Escolar")]
        public Double? PorcEscolar { get; set; }

        [DataMember]
        [DisplayName("Fecha de inicio")]
        public DateTime? FecInicio { get; set; }

        [DataMember]
        [DisplayName("Fecha de final")]
        public DateTime? FecFinal { get; set; }

    }
}
