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
    
    public partial class DetallePagoViaticoRetroactivo
    {
        public int PK_DetallePago { get; set; }
        public int FK_PagoViaticoRetroactivo { get; set; }
        public int FK_TipoDetallePagoViatico { get; set; }
        public System.DateTime FecDiaPago { get; set; }
        public decimal MonPago { get; set; }
        public int CodEntidad { get; set; }
    
        public virtual PagoViaticoRetroactivo PagoViaticoRetroactivo { get; set; }
        public virtual TipoDetallePagoViatico TipoDetallePagoViatico { get; set; }
    }
}