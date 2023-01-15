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
    
    public partial class Incidencia
    {
        public Incidencia()
        {
            this.BitacoraIncidencia = new HashSet<BitacoraIncidencia>();
        }
    
        public int PK_Incidencia { get; set; }
        public int FK_FuncionarioEmisor { get; set; }
        public Nullable<int> FK_FuncionarioUIRH { get; set; }
        public int FK_CatalogoIncidencia { get; set; }
        public int EstIncidencia { get; set; }
        public string IndIPOrigen { get; set; }
        public string DesError { get; set; }
        public string DesJustificacion { get; set; }
        public byte[] ImgError { get; set; }
        public System.DateTime FecInicio { get; set; }
        public Nullable<System.DateTime> FecFin { get; set; }
        public Nullable<int> IndImpacto { get; set; }
    
        public virtual ICollection<BitacoraIncidencia> BitacoraIncidencia { get; set; }
        public virtual CatalogoIncidencia CatalogoIncidencia { get; set; }
        public virtual Funcionario Funcionario { get; set; }
        public virtual Funcionario Funcionario1 { get; set; }
    }
}
