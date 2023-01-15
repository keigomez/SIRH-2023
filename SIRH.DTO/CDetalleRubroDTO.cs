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
    public class CDetalleRubroDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Detalle Diferencia Salarial")]
        public CDetalleDiferenciaSalarialDTO DetalleDiferenciaSalarial { get; set; }

        [DataMember]
        [DisplayName("Componente Salarial")]
        public CComponenteSalarialDTO ComponenteSalarial { get; set; }

        [DataMember]
        [DisplayName("Monto Anterior")]
        public Double? MontoAnterior { get; set; }

        [DataMember]
        [DisplayName("Monto Actual")]
        public Double? MontoActual { get; set; }
    }
}

