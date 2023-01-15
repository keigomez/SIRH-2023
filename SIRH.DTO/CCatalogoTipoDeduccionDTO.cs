using System.Runtime.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace SIRH.DTO
{
    [DataContract]
    public class CCatalogoTipoDeduccionDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Tipo de Deducción")]
        public CTipoDeduccionDTO TipoDeduccion { get; set; }

        [DataMember]
        [DisplayName("Fecha de inicio")]
        public DateTime? FecInicio { get; set; }

        [DataMember]
        [DisplayName("Fecha de final")]
        public DateTime? FecFinal { get; set; }

        [DataMember]
        [DisplayName("Total Porcentaje de Rubros")]
        public String TotalPorcRubros { get; set; }
    }
}
