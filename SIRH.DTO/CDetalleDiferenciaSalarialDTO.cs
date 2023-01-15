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
    public class CDetalleDiferenciaSalarialDTO : CBaseDTO
    {
        [DataMember]
        [DisplayName("Diferencia Salarial")]
        public CDiferenciaSalarialDTO DiferenciaSalarial { get; set; }

        [DataMember]
        [DisplayName("Porcentaje Renta")]
        public CPorcentajeRentaDTO PorcentajeRenta { get; set; }

        [DataMember]
        [DisplayName("Porcentaje Salario Escolar")]
        public CPorcentajeSalarioEscolarDTO PorcentajeSalarioEscolar { get; set; }

        [DataMember]
        [DisplayName("Fecha de inicio")]
        public DateTime? FecInicio { get; set; }

        [DataMember]
        [DisplayName("Fecha de final")]
        public DateTime? FecFinal { get; set; }

        [DataMember]
        [DisplayName("Total Diferencia")]
        public Double? TotalDiferencia { get; set; }

        [DataMember]
        [DisplayName("Total Salario Escolar")]
        public Double? TotalSalarioEscolar { get; set; }

        [DataMember]
        [DisplayName("Total Renta")]
        public Double? TotalRenta { get; set; }

        [DataMember]
        [DisplayName("Total Deducciones Obrero")]
        public Double? TotalDeduccionesObrero { get; set; }

        [DataMember]
        [DisplayName("Total Deducciones Patronal")]
        public Double? TotalDeduccionesPatronal { get; set; }

        [DataMember]
        [DisplayName("Total Aguinaldo")]
        public Double? TotalAguinaldo { get; set; }

    }
}

