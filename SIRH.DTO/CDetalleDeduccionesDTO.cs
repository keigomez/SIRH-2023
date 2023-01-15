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
    public class CDetalleDeduccionesDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Detalle Diferencia Salarial")]
        public CDetalleDiferenciaSalarialDTO DetalleDiferenciaSalarial { get; set; }

        [DataMember]
        [DisplayName("Catalogo Tipo Deducción")]
        public CCatalogoTipoDeduccionDTO CatalogoTipoDeduccion { get; set; }

        [DataMember]
        [DisplayName("Fecha de registro")]
        public DateTime? FecRegistro { get; set; }
    }
}
