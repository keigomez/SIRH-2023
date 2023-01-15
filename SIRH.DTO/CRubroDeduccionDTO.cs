using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SIRH.DTO
{
    [DataContract]
    public class CRubroDeduccionDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Catalogo Tipo de Deducción")]
        public CCatalogoTipoDeduccionDTO CatalogoTipoDeduccion { get; set; }

        [DataMember]
        [DisplayName("Catalogo Rubro Deduccion")]
        public CCatalogoRubroDeduccionDTO CatalogoRubroDeduccion { get; set; }

        [DataMember]
        [DisplayName("Porcentaje del Rubro")]
        public Double? PorcRubro { get; set; }
    }
}
