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
    
    public partial class Decreto
    {
        public int PK_Decreto { get; set; }
        public string NumDecreto { get; set; }
        public string TituloDecreto { get; set; }
        public Nullable<System.DateTime> FecDecreto { get; set; }
        public string ObsDecreto { get; set; }
    
        public virtual DecretoComponentePresupuestario DecretoComponentePresupuestario { get; set; }
    }
}
