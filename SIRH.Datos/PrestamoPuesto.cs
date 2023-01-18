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
    
    public partial class PrestamoPuesto
    {
        public PrestamoPuesto()
        {
            this.AddendumPrestamoPuesto = new HashSet<AddendumPrestamoPuesto>();
            this.Rescision = new HashSet<Rescision>();
        }
    
        public int PK_PrestamoPuesto { get; set; }
        public Nullable<int> FK_EntidadAdscrita { get; set; }
        public Nullable<int> FK_EntidadGubernamental { get; set; }
        public Nullable<int> FK_UbicacionAdministrativa { get; set; }
        public Nullable<System.DateTime> FecTraslado { get; set; }
        public string NumResolucion { get; set; }
        public Nullable<int> FK_Puesto { get; set; }
        public string NumOficioRescision { get; set; }
        public Nullable<System.DateTime> FecFinConvenio { get; set; }
        public string NumRescision { get; set; }
    
        public virtual ICollection<AddendumPrestamoPuesto> AddendumPrestamoPuesto { get; set; }
        public virtual EntidadAdscrita EntidadAdscrita { get; set; }
        public virtual EntidadGubernamental EntidadGubernamental { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual UbicacionAdministrativa UbicacionAdministrativa { get; set; }
        public virtual ICollection<Rescision> Rescision { get; set; }
    }
}