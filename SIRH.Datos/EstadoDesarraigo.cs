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
    
    public partial class EstadoDesarraigo
    {
        public EstadoDesarraigo()
        {
            this.Desarraigo = new HashSet<Desarraigo>();
            this.DetalleDesarraigoEliminacion = new HashSet<DetalleDesarraigoEliminacion>();
        }
    
        public int PK_EstadoDesarraigo { get; set; }
        public string NomEstadoDesarraigo { get; set; }
    
        public virtual ICollection<Desarraigo> Desarraigo { get; set; }
        public virtual ICollection<DetalleDesarraigoEliminacion> DetalleDesarraigoEliminacion { get; set; }
    }
}
