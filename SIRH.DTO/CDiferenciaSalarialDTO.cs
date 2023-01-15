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
    public class CDiferenciaSalarialDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Funcionario")]
        public CFuncionarioDTO Funcionario { get; set; }

        [DataMember]
        [DisplayName("Puesto")]
        public CPuestoDTO Puesto { get; set; }

        [DataMember]
        [DisplayName("Clase")]
        public CClaseDTO Clase { get; set; }

        [DataMember]
        [DisplayName("Especialidad")]
        public CEspecialidadDTO Especialidad { get; set; }

        [DataMember]
        [DisplayName("TotalAPagar")]
        public Double? TotalAPagar { get; set; }

        [DataMember]
        [DisplayName("Estado")]
        public int? Estado { get; set; }

        [DataMember]
        [DisplayName("Fecha de registro")]
        public DateTime? FecRegistro { get; set; }
    }
}

