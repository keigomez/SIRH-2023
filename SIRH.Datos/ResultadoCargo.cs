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
    
    public partial class ResultadoCargo
    {
        public ResultadoCargo()
        {
            this.ActividadResultadoCargo = new HashSet<ActividadResultadoCargo>();
        }
    
        public int PK_ResultadoCargo { get; set; }
        public string DesResultadoCargo { get; set; }
        public Nullable<int> FK_Cargo { get; set; }
    
        public virtual ICollection<ActividadResultadoCargo> ActividadResultadoCargo { get; set; }
        public virtual Cargo Cargo { get; set; }
    }
}