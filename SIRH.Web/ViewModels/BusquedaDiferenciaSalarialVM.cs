using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIRH.DTO;

namespace SIRH.Web.ViewModels
{
    public class BusquedaDiferenciaSalarialVM
    {
        public List<CDiferenciaSalarialDTO> Diferencias { get; set; }

        public CDiferenciaSalarialDTO Filtro { get; set; }

        public DateTime? FecInicio { get; set; }

        public DateTime? FecFinal { get; set; }

        public int ItemsPorPagina { get; set; }

        public int TotalItems { get; set; }

        public int TotalPaginas { get; set; }

        public int PaginaActual { get; set; }
    }
}