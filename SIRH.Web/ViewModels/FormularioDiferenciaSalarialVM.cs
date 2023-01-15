using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SIRH.DTO;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIRH.Web.ViewModels
{
    public class FormularioDiferenciaSalarialVM
    {
        public CDiferenciaSalarialDTO DiferenciaSalarial { get; set; }
        public CFuncionarioDTO Funcionario { get; set; }
        public CPuestoDTO PuestoAnterior { get; set; }
        public CPuestoDTO PuestoActual { get; set; }
        public CDetallePuestoDTO DetallePuestoAnterior { get; set; }
        public CDetallePuestoDTO DetallePuestoActual { get; set; }
        public CDetalleDiferenciaSalarialDTO PeriodoActual { get; set; }
        public List<CDetalleRubroDTO> Rubros { get; set; }
        public List<bool> AplicaRubro { get; set; }
        public int Paso { get; set; }

    }
}