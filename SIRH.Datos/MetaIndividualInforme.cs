//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIRH.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class MetaIndividualInforme
    {
        public int PK_Informe { get; set; }
        public int FK_Meta { get; set; }
        public System.DateTime FecMes { get; set; }
        public decimal NumIndicador { get; set; }
        public decimal NumResultadoProduccion { get; set; }
        public string DesInforme { get; set; }
        public int IndCompleto { get; set; }
        public int IndEstado { get; set; }
        public string DesObservaciones { get; set; }
    
        public virtual MetaIndividualCalificacion MetaIndividualCalificacion { get; set; }
    }
}
