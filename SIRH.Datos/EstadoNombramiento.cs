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
    
    public partial class EstadoNombramiento
    {
        public EstadoNombramiento()
        {
            this.Nombramiento = new HashSet<Nombramiento>();
        }
    
        public int PK_EstadoNombramiento { get; set; }
        public string DesEstado { get; set; }
    
        public virtual ICollection<Nombramiento> Nombramiento { get; set; }
    }
}