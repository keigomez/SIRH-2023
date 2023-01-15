using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIRH.DTO;
using SIRH.Web.ComponentePresupuestarioLocal;
using SIRH.Web.Helpers;
using SIRH.Web.UserValidation;
using SIRH.Web.ViewModels;

namespace SIRH.Web.Controllers
{
    public class ComponentePresupuestarioController : Controller
    {
        CComponentePresupuestarioServiceClient componentePresupuestarioService = new CComponentePresupuestarioServiceClient();
        CAccesoWeb context = new CAccesoWeb();
        WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

        public ActionResult Index()
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.Cauciones), 0);

            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.Cauciones)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.Cauciones) });
            }
            else
            {
                context.GuardarBitacora(principal.Identity.Name, Convert.ToInt32(EModulosHelper.Cauciones), Convert.ToInt32(EAccionesBitacora.Login), 0,
                    CAccesoWeb.ListarEntidades(typeof(CCaucionDTO).Name));
                return View();
            }
        }


        // GET: /ComponentePresupuestario/CreateDecreto

        public ActionResult CreateDecreto()
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.ComponentePresupuestario), 0);

            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.ComponentePresupuestario)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.ComponentePresupuestario) });
            }
            else
            {
                if (Convert.ToBoolean(Session["Administrador_Global"]) ||
                    Convert.ToBoolean(Session["Administrador_" + Convert.ToInt32(EModulosHelper.ComponentePresupuestario)]) ||
                    Session[CAccesoWeb.GenerarCadenaPermiso(EModulosHelper.Cauciones, Convert.ToInt32(ENivelesCaucion.Operativo))] != null)
                {
                    ComponentePresupuestarioVM model = new ComponentePresupuestarioVM();

                    var programas = componentePresupuestarioService.ListarProgramas()
                        .Select(Q => new SelectListItem
                        {
                            Value = ((CProgramaDTO)Q).IdEntidad.ToString(),
                            Text = ((CProgramaDTO)Q).DesPrograma
                        });

                    model.Programas = new SelectList(programas, "Value", "Text");

                    var objetogasto = componentePresupuestarioService.ListarObjetoGasto()
                        .Select(Q => new SelectListItem
                        {
                            Value = ((CObjetoGastoDTO)Q).IdEntidad.ToString(),
                            Text = ((CObjetoGastoDTO)Q).DesObjGasto
                        });

                    model.ObjetoGastosS = new SelectList(objetogasto, "Value", "Text");

                    //var tiposMovimientos = componentePresupuestarioService.DescargarCatMovimientoPresupuesto()
                    //   .Select(Q => new SelectListItem
                    //   {
                    //       Value = ((CCatMovimientoPresupuestoDTO)Q).IdEntidad.ToString(),
                    //       Text = ((CCatMovimientoPresupuestoDTO)Q).DesMovimientoPresupuesto
                    //   });

                    //model.TiposMovimiento = new SelectList(tiposMovimientos, "Value", "Text");


                    return View(model);
                }
                else
                {
                    CAccesoWeb.CargarErrorAcceso(Session);
                    return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.Cauciones) });
                }
            }
        }


        // POST: /ComponentePresupuestario/CreateDecreto

        [HttpPost]
        public ActionResult CreateDecreto(ComponentePresupuestarioVM model, string SubmitButton)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    CProgramaDTO programa = new CProgramaDTO
                    {
                        IdEntidad = model.ProgramaSeleccionado
                    };

                    model.ComponentePresupuestario.Programa = programa;

                    CObjetoGastoDTO objetogasto = new CObjetoGastoDTO
                    {
                        IdEntidad = model.ObjetoGastoSeleccionado
                    };

                    model.ComponentePresupuestario.ObjetoGasto = objetogasto;

                    var resultado = componentePresupuestarioService.AgregarDecretoComponentePresupuestario(programa, objetogasto, model.ComponentePresupuestario.TipoMovimiento, model.ComponentePresupuestario);

                    if (resultado.GetType() != typeof(CErrorDTO))
                    {
                        context.GuardarBitacora(principal.Identity.Name, Convert.ToInt32(EModulosHelper.ComponentePresupuestario),
                            Convert.ToInt32(EAccionesBitacora.Guardar), resultado.IdEntidad,
                            CAccesoWeb.ListarEntidades(typeof(CComponentePresupuestarioDTO).Name));
                         return RedirectToAction("Details", new { codDecreto = resultado.IdEntidad, accion = "guardar" });
                        //return JavaScript("Se Guardo Correctamente");


                    }
                    else
                    {
                        throw new Exception(((CErrorDTO)resultado).MensajeError);
                    }
                }
                else
                {
                    throw new Exception("Validacion");
                }
            }
            catch (Exception error)
            {

                if (error.Message != "Validacion")
                {
                    ModelState.AddModelError("BDError", error.Message);
                }

                return View(model);
            }



        }


        //GET: /ComponentePresupuestario/Search
        public ActionResult SearchPresupuesto()
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.ComponentePresupuestario), 0);

            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.ComponentePresupuestario)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.ComponentePresupuestario) });
            }
            else
            {
                if (Convert.ToBoolean(Session["Administrador_Global"]) ||
                    Convert.ToBoolean(Session["Administrador_" + Convert.ToInt32(EModulosHelper.ComponentePresupuestario)]) ||
                    Session[CAccesoWeb.GenerarCadenaPermiso(EModulosHelper.ComponentePresupuestario, Convert.ToInt32(ENivelesCaucion.Consulta))] != null ||
                    Session[CAccesoWeb.GenerarCadenaPermiso(EModulosHelper.ComponentePresupuestario, Convert.ToInt32(ENivelesCaucion.Operativo))] != null)
                {
                    PresupuestoVM model = new PresupuestoVM();

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.ComponentePresupuestario) });
                }
            }
        }


        //POST: /ComponentePresupuestario/Search

        [HttpPost]
        public ActionResult SearchPresupuesto(PresupuestoVM model)
        {
            try
            {
                ModelState.Clear();



                // VALIDA CAMPOS DEL FORMULARIO (QUE NO ESTÉN VACÍOS)
                if (model.ComponentePresupuestario.AnioPresupuesto != null )
                // || model.ComponentePresupuestario.Programa != null)
                {
                    //hacer validación para años

                    // LLAMA A LOS DATOS DE UN SERVICIO WEB (BD)   
                    var datos = componentePresupuestarioService.ListarMovimientosPresupuesto(model.ComponentePresupuestario.AnioPresupuesto);

                    if (datos.FirstOrDefault().GetType() != typeof(CErrorDTO))
                    {
                        List<CComponentePresupuestarioDTO> resultsModel = new List<CComponentePresupuestarioDTO>();

                        foreach (var item in datos)
                        {

                            resultsModel.Add((CComponentePresupuestarioDTO)item);
                        }

                        Session["ComponentePresupuestario"] = resultsModel;
                        return PartialView("_SearchPresupuestoResult", resultsModel);
                    }
                    else
                    {
                        //GENERACIÓN DE ERRORES
                        ModelState.AddModelError("Datos", ((CErrorDTO)datos.FirstOrDefault()).MensajeError);
                        throw new Exception("Busqueda");
                    }
                }
                else
                {
                    ModelState.AddModelError("Datos", "Debe seleccionar el parámetro de búsqueda establecido.");
                    throw new Exception("Busqueda");
                }
            }
            catch (Exception error)
            {
                if (error.Message == "Busqueda")
                {
                    return PartialView("_ErrorPresupuesto");
                }
                else
                {
                    return View(model);
                }
            }
        }


        public ActionResult GenerarReportePresupuesto()
        {
            PresupuestoVM modelPresupuesto = new PresupuestoVM();

            string style = @"<style> TD { mso-number-format:\@; } </style>";
            //var headerTable = @"<Table><tr><td><img src=""https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQNqlnu1iMrzA69ctdlBJvavSQ8d8bKAUtilw&usqp=CAU"" \></td></tr></Table>";
            var products = new System.Data.DataTable("teste");


            products.Columns.Add("AÑO", typeof(string));
            products.Columns.Add("OBJETO GASTO", typeof(string));
            products.Columns.Add("PROGRAMA", typeof(string));
            products.Columns.Add("MONTO", typeof(string));
            products.Columns.Add("TIPO DE MOVIMIENTO", typeof(string));
            products.Columns.Add("DETALLE", typeof(string));
            List<CComponentePresupuestarioDTO> listaPresupuestos = (List<CComponentePresupuestarioDTO>)Session["ComponentePresupuestario"];


            foreach (var item in listaPresupuestos)
            {
                products.Rows.Add(item.AnioPresupuesto,
                    item.ObjetoGasto.DesObjGasto,
                    item.Programa.DesPrograma,
                    item.MontoComponente,
                    item.TipoMovimiento.DesMovimientoPresupuesto,
                    item.Detalle
                    );
            }


            products.Rows.Add("", "", "", "", "", "");


            products.Rows.Add("SIRH", "Fecha:", DateTime.Now.ToShortDateString(), principal.Identity.Name.Split('\\')[1]);


            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();


            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ControlPresupuesto.xls");
            Response.AddHeader("Transfer-Encoding", "identity");
            Response.ContentType = "application/ms-excel";


            //Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);


            grid.RenderControl(htw);
            Response.Write(style);
            //Response.Write(headerTable);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.Close();
            Response.End();
            return View();
        }

        public ActionResult Details(int codDecreto)
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.ComponentePresupuestario), 0);

            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.ComponentePresupuestario)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.ComponentePresupuestario) });
            }
            else
            {
                if (Convert.ToBoolean(Session["Administrador_Global"]) ||
                    Convert.ToBoolean(Session["Administrador_" + Convert.ToInt32(EModulosHelper.ComponentePresupuestario)]) ||
                    Session[CAccesoWeb.GenerarCadenaPermiso(EModulosHelper.ComponentePresupuestario, Convert.ToInt32(ENivelesCaucion.Consulta))] != null ||
                    Session[CAccesoWeb.GenerarCadenaPermiso(EModulosHelper.ComponentePresupuestario, Convert.ToInt32(ENivelesCaucion.Operativo))] != null)
                {
                    ComponentePresupuestarioVM model = new ComponentePresupuestarioVM();

                    var datos = componentePresupuestarioService.ObtenerDecreto(codDecreto);

                    if (datos.GetType() != typeof(CErrorDTO))
                    {
                        model.ComponentePresupuestario = (CComponentePresupuestarioDTO)datos;
                    }
                    else
                    {
                        model.Error = (CErrorDTO)datos;
                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.ComponentePresupuestario) });
                }
            }
        }
    }


}



