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
    
    public partial class DetalleAsignacionGastoTransporteModificada
    {
        public int PK_DetalleAsignacionGastoModificada { get; set; }
        public string NomRuta { get; set; }
        public string NomFraccionamiento { get; set; }
        public string MontTarifa { get; set; }
        public int FK_GastoTransporte { get; set; }
        public string NumGaceta { get; set; }
    
        public virtual GastoTransporte GastoTransporte { get; set; }
    }
}
