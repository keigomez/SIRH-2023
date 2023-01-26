using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
//using SIRH.Web.CaucionService;
using SIRH.Web.CaucionesLocal;
//using SIRH.Web.FuncionarioService;
using SIRH.Web.FuncionarioLocal;
using SIRH.Web.PuestoLocal;
using SIRH.Web.PerfilUsuarioLocal;
using SIRH.Web.DiferenciaSalarialLocal;
//using SIRH.Web.PuestoService;
//using SIRH.Web.EstudioPuestoLocal;
using SIRH.DTO;
using SIRH.Web.ViewModels;
using SIRH.Web.Helpers;
using System.Security.Principal;
using SIRH.Web.UserValidation;
using System.IO;
using SIRH.Web.Reports.PDF;
//using SIRH.Web.Reports.EstudioPuesto;
using SIRH.Web.Models;

namespace SIRH.Web.Controllers
{
    public class DiferenciaSalarialController : Controller
    {
        //CFuncionarioServiceClient servicioFuncionario = new CFuncionarioServiceClient();
        //CCaucionesServiceClient servicioCaucion = new CCaucionesServiceClient();
        //CPerfilUsuarioServiceClient servicioPerfilUsuario = new CPerfilUsuarioServiceClient();

        CPuestoServiceClient servicioPuesto = new CPuestoServiceClient();
        CDiferenciaSalarialServiceClient servicioDiferenciaSalarial = new CDiferenciaSalarialServiceClient();
        CAccesoWeb context = new CAccesoWeb();
        WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());

        #region GET

        public ActionResult Index()
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.DiferenciaSalarial), 0);
            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.DiferenciaSalarial)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.DiferenciaSalarial) });
            }
            else
            {
                context.GuardarBitacora(principal.Identity.Name, Convert.ToInt32(EModulosHelper.DiferenciaSalarial), Convert.ToInt32(EAccionesBitacora.Login), 0,
                    CAccesoWeb.ListarEntidades(typeof(CCaucionDTO).Name));
                return View();
            }
        }

        public ActionResult AgregarDiferenciaSalarial()
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.DiferenciaSalarial), 0);

            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.DiferenciaSalarial)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.DiferenciaSalarial) });
            }
            else
            {
                if (Convert.ToBoolean(Session["Administrador_Global"]) ||
                    Convert.ToBoolean(Session["Administrador_" + Convert.ToInt32(EModulosHelper.DiferenciaSalarial)]) ||
                    Session[CAccesoWeb.GenerarCadenaPermiso(EModulosHelper.DiferenciaSalarial, Convert.ToInt32(ENivelesDiferenciaSalarial.Operativo))] != null)
                {
                    Session["periodos"] = new List<CDetalleDiferenciaSalarialDTO>();
                    return View(new FormularioDiferenciaSalarialVM
                    {
                        Paso = 1,
                        PuestoAnterior = new CPuestoDTO(),
                        PuestoActual = new CPuestoDTO(),
                        DetallePuestoActual = new CDetallePuestoDTO(),
                        DetallePuestoAnterior = new CDetallePuestoDTO(),
                    });
                }
                else
                {
                    CAccesoWeb.CargarErrorAcceso(Session);
                    return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.DiferenciaSalarial) });
                }
            }
        }

        public ActionResult BuscarDiferenciasSalariales()
        {
            context.IniciarSesionModulo(Session, principal.Identity.Name, Convert.ToInt32(EModulosHelper.DiferenciaSalarial), 0);
            if (Session["Perfil_" + Convert.ToInt32(EModulosHelper.DiferenciaSalarial)].ToString().StartsWith("Error"))
            {
                return RedirectToAction("Index", "Error", new { modulo = Convert.ToInt32(EModulosHelper.DiferenciaSalarial) });
            }
            else
            {
                context.GuardarBitacora(principal.Identity.Name, Convert.ToInt32(EModulosHelper.DiferenciaSalarial), Convert.ToInt32(EAccionesBitacora.Login), 0,
                    CAccesoWeb.ListarEntidades(typeof(CCaucionDTO).Name));
                FormularioDiferenciaSalarialVM model = new FormularioDiferenciaSalarialVM
                {
                    DiferenciaSalarial = new CDiferenciaSalarialDTO(),
                    PeriodoActual = new CDetalleDiferenciaSalarialDTO()
                };
                return View();
            }
        }


        public PartialViewResult Clase_Index(string codigoclase, string nomclase, int? page)
        {
            Session["errorF"] = null;
            try
            {
                ClaseModel modelo = new ClaseModel();

                int paginaActual = page.HasValue ? page.Value : 1;

                if (String.IsNullOrEmpty(codigoclase) && String.IsNullOrEmpty(nomclase))
                {
                    return PartialView();
                }
                else
                {
                    modelo.CodigoSearch = codigoclase;
                    modelo.NombreSearch = nomclase;
                    int codigoClase = String.IsNullOrEmpty(modelo.CodigoSearch) ? 0 : Convert.ToInt32(modelo.CodigoSearch);
                    var clases = servicioPuesto.BuscarClaseParams(codigoClase, modelo.NombreSearch);
                    modelo.TotalClases = clases.Count();
                    modelo.TotalPaginas = (int)Math.Ceiling((double)modelo.TotalClases / 10);
                    modelo.PaginaActual = paginaActual;
                    if ((((paginaActual - 1) * 10) + 10) > modelo.TotalClases)
                    {
                        modelo.Clase = clases.ToList().GetRange(((paginaActual - 1) * 10), (modelo.TotalClases) - (((paginaActual - 1) * 10))).ToList();
                    }
                    else
                    {
                        modelo.Clase = clases.ToList().GetRange(((paginaActual - 1) * 10), 10).ToList(); ;
                    }

                    return PartialView("Clase_Index_Result", modelo);
                }
            }
            catch (Exception error)
            {
                ModelState.AddModelError("Busqueda", "Ha ocurrido un error a la hora de realizar la búsqueda, ponerse en contacto con el personal autorizado. \n\n");
                return PartialView("_ErrorPuesto");
            }
        }

        public PartialViewResult Especialidad_Index(string codigoEspecialidad, string nomEspecialidad, int? page)
        {
            Session["errorP"] = null;
            try
            {
                EspecialidadModel modelo = new EspecialidadModel();

                int paginaActual = page.HasValue ? page.Value : 1;

                if (String.IsNullOrEmpty(codigoEspecialidad) && String.IsNullOrEmpty(nomEspecialidad))
                {
                    return PartialView();
                }
                else
                {
                    modelo.CodigoSearch = codigoEspecialidad;
                    modelo.NombreSearch = nomEspecialidad;
                    int codigoEsp = String.IsNullOrEmpty(modelo.CodigoSearch) ? 0 : Convert.ToInt32(modelo.CodigoSearch);
                    var especialidades = servicioPuesto.BuscarEspecialidadParams(codigoEsp, modelo.NombreSearch);
                    modelo.TotalEspecialidades = especialidades.Count();
                    modelo.TotalPaginas = (int)Math.Ceiling((double)modelo.TotalEspecialidades / 10);
                    modelo.PaginaActual = paginaActual;
                    if ((((paginaActual - 1) * 10) + 10) > modelo.TotalEspecialidades)
                    {
                        modelo.Especialidad = especialidades.ToList().GetRange(((paginaActual - 1) * 10), (modelo.TotalEspecialidades) - (((paginaActual - 1) * 10))).ToList(); ;
                    }
                    else
                    {
                        modelo.Especialidad = especialidades.ToList().GetRange(((paginaActual - 1) * 10), 10).ToList(); ;
                    }

                    return PartialView("Especialidad_Index_Result", modelo);
                }
            }
            catch (Exception error)
            {
                ModelState.AddModelError("Busqueda", "Ha ocurrido un error a la hora de realizar la búsqueda, ponerse en contacto con el personal autorizado. \n\n");
                return PartialView("_ErrorPuesto");
            }
        }



        public PartialViewResult Puesto_Index(string codigoPuesto, int? page)
        {
            Session["errorP"] = null;
            try
            {
                PuestoModel modelo = new PuestoModel();

                int paginaActual = page.HasValue ? page.Value : 1;

                if (String.IsNullOrEmpty(codigoPuesto))
                {
                    return PartialView();
                }
                else
                {
                    modelo.CodigoSearch = codigoPuesto;
                    
                    int codigoEsp = String.IsNullOrEmpty(modelo.CodigoSearch) ? 0 : Convert.ToInt32(modelo.CodigoSearch);
                    var puestos = servicioPuesto.BuscarPuestoCodigoParams(codigoEsp.ToString());
                    modelo.TotalPuestos = puestos.Count();
                    modelo.TotalPaginas = (int)Math.Ceiling((double)modelo.TotalPuestos / 10);
                    modelo.PaginaActual = paginaActual;
                    if ((((paginaActual - 1) * 10) + 10) > modelo.TotalPuestos)
                    {
                        modelo.Puesto = puestos.ToList().GetRange(((paginaActual - 1) * 10), (modelo.TotalPuestos) - (((paginaActual - 1) * 10))).ToList(); ;
                    }
                    else
                    {
                        modelo.Puesto = puestos.ToList().GetRange(((paginaActual - 1) * 10), 10).ToList(); ;
                    }

                    return PartialView("Puesto_Index_Result", modelo);
                }
            }
            catch (Exception error)
            {
                ModelState.AddModelError("Busqueda", "Ha ocurrido un error a la hora de realizar la búsqueda, ponerse en contacto con el personal autorizado. \n\n");
                return PartialView("_ErrorPuesto");
            }
        }

        #endregion

        #region POST

        /*
            Este metodo se encarga de gestionar todos los pasos al agregar una diferencia salarial
            Paso 1: Ingresar o Editar los Puestos
            Paso 2: Ingresar o Editar las fechas del periodo nuevo
            Paso 3: Ingresar o Editar los componentes salariales del periodo
            Paso 4: Ver los periodos
            Paso 5: Ver Resumen
         */
        [HttpPost]
        public ActionResult AgregarDiferenciaSalarial(FormularioDiferenciaSalarialVM model, string SubmitButton)
        {
            try
            {
                if (SubmitButton == "Regresar")
                {
                    model.Paso--;
                }
                else if (SubmitButton == "Continuar")
                {
                    model.Paso++;
                }

                if (SubmitButton == "Editar Puestos")
                {
                    model.Paso = 1;
                }
                else if (SubmitButton == "Cambiar fechas")
                {
                    model.Paso = 2;
                }
                else if (SubmitButton == "Generar Resumen")
                {
                    model.Paso = 5;
                }
                else if (SubmitButton == "Ver periodos")
                {
                    model.Paso = 4;
                }
                else if (SubmitButton == "Agregar otro periodo")
                {
                    model.Paso = 2;
                }
                else if (SubmitButton == "Nuevo periodo")
                {
                    return CrearNuevoPeriodo(model);
                }
                else if (SubmitButton.Split('-')[0] == "Editar periodo") // Editar periodo-indxPeriodo
                {
                    return EditarPeriodo(model, SubmitButton);
                }
                else if (SubmitButton.Split('-')[0] == "Eliminar periodo")// Eliminar periodo-indxPeriodo
                {
                    return EliminarPeriodo(model, SubmitButton);
                }
                else if (SubmitButton == "Agregar periodo")
                {
                    return AgregarNuevoPeriodo(model);
                }
                else if (SubmitButton == "Finalizar y Salir")
                {
                    return GuardarDiferenciaSalarial(model);
                }

                ModelState.Clear();
                return View(model);
            }
            catch (Exception error)
            {
                ModelState.AddModelError("buscar", (error.Message));
                return PartialView("_ErrorDiferenciaSalarial");
            }
        }

        /*
            Limpia el formulario para agregar el nuevo periodo
            Buscando los porcentajes de renta y de salario escolar que aplican en ese periodo
            Ademas carga los componentes salariales para que se puedan ingresar los montos (Anterior y Actual)
        */
        public ActionResult CrearNuevoPeriodo(FormularioDiferenciaSalarialVM model)
        {
            try
            {
                var rubros = (CBaseDTO[])(Session["rubros"] ?? (Session["rubros"] = servicioDiferenciaSalarial.ListarComponentesSalariales()));
                if (rubros.Count() > 0 && rubros.ElementAt(0).GetType() != typeof(CErrorDTO))
                {
                    model.AplicaRubro = new List<bool>();
                    model.Rubros = new List<CDetalleRubroDTO>();
                    foreach (CComponenteSalarialDTO rubro in rubros)
                    {
                        model.Rubros.Add(new CDetalleRubroDTO
                        {
                            ComponenteSalarial = rubro,
                        });
                        model.AplicaRubro.Add(false);
                    }
                }
                else
                {
                    throw new Exception(((CErrorDTO)rubros.ElementAt(0)).MensajeError);
                }
                var porcSalarioEscolar = servicioDiferenciaSalarial.BuscarPorcentajeSalarioEscolar(model.PeriodoActual.FecInicio ?? DateTime.Now);
                if (porcSalarioEscolar.GetType() != typeof(CErrorDTO))
                {
                    Session["salarioescolar"] = (CPorcentajeSalarioEscolarDTO)porcSalarioEscolar;
                }
                else
                {
                    throw new Exception(((CErrorDTO)porcSalarioEscolar).MensajeError);// No se encontro
                }

                var porcentajesRenta = servicioDiferenciaSalarial.BuscarPorcentajeRenta(model.PeriodoActual.FecInicio ?? DateTime.Now);
                if (porcentajesRenta.Count() > 0 && porcentajesRenta.ElementAt(0).GetType() != typeof(CErrorDTO))
                {
                    var porcentajesRentaPeriodoActual = new List<CPorcentajeRentaDTO>();
                    foreach (CPorcentajeRentaDTO porcRenta in porcentajesRenta)
                    {
                        porcentajesRentaPeriodoActual.Add(porcRenta);
                    }
                    Session["rentas"] = porcentajesRentaPeriodoActual;
                }
                else
                {
                    throw new Exception(((CErrorDTO)porcentajesRenta[0]).MensajeError);// No se encontro
                }

                // Obtener las deducciones Patronales y las del Obrero
                var catalogoDeduccionesPatronal = servicioDiferenciaSalarial.BuscarRubrosDeducciones(model.PeriodoActual.FecInicio ?? DateTime.Now, 1);
                if (catalogoDeduccionesPatronal.Count() > 0 ||
                    catalogoDeduccionesPatronal.ElementAt(0).Count() > 0 ||
                    catalogoDeduccionesPatronal.ElementAt(0).ElementAt(0).GetType() != typeof(CErrorDTO))
                {
                    List<List<CBaseDTO>> catalogo = new List<List<CBaseDTO>>
                        {
                            new List<CBaseDTO> { catalogoDeduccionesPatronal.ElementAt(0).ElementAt(0) } // Agregar el Catalogo tipo de deduccion
                        };
                    List<CBaseDTO> deducciones = new List<CBaseDTO>();
                    foreach (CBaseDTO deduccion in catalogoDeduccionesPatronal.ElementAt(1)) // Agregar las deducciones
                    {
                        deducciones.Add(deduccion);
                    }
                    catalogo.Add(deducciones);
                    // Catalogos [0] -> [CatalogoTipoPeriodo],  [1] -> [List<Deducciones>]
                    Session["deduccionesPatronales"] = catalogo;
                }
                else
                {
                    throw new Exception("No se encontraron las deducciones patronales");// No se encontro
                }

                var catalogoDeduccionesObrero = servicioDiferenciaSalarial.BuscarRubrosDeducciones(model.PeriodoActual.FecInicio ?? DateTime.Now, 2);
                if (catalogoDeduccionesObrero.Count() > 0 ||
                    catalogoDeduccionesObrero.ElementAt(0).Count() > 0 ||
                    catalogoDeduccionesObrero.ElementAt(0).ElementAt(0).GetType() != typeof(CErrorDTO))
                {
                    List<List<CBaseDTO>> catalogo = new List<List<CBaseDTO>>
                        {
                            new List<CBaseDTO> { catalogoDeduccionesObrero.ElementAt(0).ElementAt(0) } // Agregar el Catalogo tipo de deduccion
                        };
                    List<CBaseDTO> deducciones = new List<CBaseDTO>();
                    foreach (CBaseDTO deduccion in catalogoDeduccionesObrero.ElementAt(1)) // Agregar las deducciones
                    {
                        deducciones.Add(deduccion);
                    }
                    catalogo.Add(deducciones);
                    // Catalogos [0] -> [CatalogoTipoPeriodo],  [1] -> [List<Deducciones>]
                    Session["deduccionesObrero"] = catalogo;
                }
                else
                {
                    throw new Exception("No se encontraron las deducciones del obrero");// No se encontro
                }
                model.Paso = 3;
                ModelState.Clear(); // Limpiar cache
                return View(model);
            }
            catch (Exception error)
            {
                ModelState.AddModelError("buscar", (error.Message));
                return PartialView("_ErrorDiferenciaSalarial");
            }
        }


        /*
            Tomar los datos del nuevo periodo para agregarlo a la lista de periodos
            Tomando los componentes salariales del nuevo periodo con los montos y realiza los calculos
            Para ingresarlo a la lista de periodos (detalles)
        */
        public ActionResult AgregarNuevoPeriodo(FormularioDiferenciaSalarialVM model)
        {
            try
            {
                // Obtener el TotalSalario tanto de Anterior como Actual
                // Reliza una sumatoria de los montos anteriores y actuales de los rubros que aplicaron (Salario Base, Salario Escolar,...)
                double totalSalarioAnterior = 0.0;
                double totalSalarioActual = 0.0;
                var rubrosActuales = new List<CDetalleRubroDTO>();
                for (int r = 0; r < model.Rubros.Count(); r++)
                {
                    if (model.AplicaRubro[r])
                    {
                        rubrosActuales.Add(model.Rubros[r]);
                        totalSalarioAnterior += model.Rubros[r].MontoAnterior ?? 0;
                        totalSalarioActual += model.Rubros[r].MontoActual ?? 0;
                    }
                }
                // Obtiene la diferencia del periodo
                double totalSalarioDiferencia = totalSalarioActual - totalSalarioAnterior;

                // Obtener el SalarioEscolar Anterior y Actual
                model.PeriodoActual.PorcentajeSalarioEscolar = (CPorcentajeSalarioEscolarDTO)Session["salarioescolar"];
                double salarioEscolarAnterior = totalSalarioAnterior * (model.PeriodoActual.PorcentajeSalarioEscolar.PorcEscolar ?? 0) / 100;
                double salarioEscolarActual = totalSalarioActual * (model.PeriodoActual.PorcentajeSalarioEscolar.PorcEscolar ?? 0) / 100;
                double salarioEscolarDiferencia = salarioEscolarActual - salarioEscolarAnterior;

                // Aplicar renta al salario escolar si esta se encuentra entre el periodo 01 JULIO DE 1994 AL 30 JUNIO 1996
                // Por lo tanto el salario con el que se calcula la renta debe incluir el salario escolar
                double totalSalarioAnteriorParaRenta = totalSalarioAnterior;
                double totalSalarioActualParaRenta = totalSalarioActual;
                DateTime inicioRentaSalarioEscolar = new DateTime(1994, 07, 01);
                DateTime finalRentaSalarioEscolar = new DateTime(1996, 06, 30);
                if (inicioRentaSalarioEscolar <= model.PeriodoActual.FecInicio && model.PeriodoActual.FecInicio <= finalRentaSalarioEscolar)
                {
                    totalSalarioAnteriorParaRenta += salarioEscolarAnterior;
                    totalSalarioActualParaRenta += salarioEscolarActual;
                }

                // Obtener la Renta Anterior y Actual
                double totalRentaAnterior = 0.0;
                double totalRentaActual = 0.0;
                foreach (CPorcentajeRentaDTO porcRenta in (List<CPorcentajeRentaDTO>)Session["rentas"])
                {
                    // Para la renta del salario Anterior
                    // Si el salario se encuentra entre el intervalo de los rangos del porcentaje de renta
                    if ((porcRenta.RangoInicial == null || porcRenta.RangoInicial <= totalSalarioAnteriorParaRenta) &&
                        (porcRenta.RangoFinal == null || totalSalarioAnteriorParaRenta <= porcRenta.RangoFinal))
                    {
                        totalRentaAnterior += (totalSalarioAnteriorParaRenta - (porcRenta.RangoInicial ?? 0)) * (porcRenta.PorcRenta ?? 0) / 100;
                        model.PeriodoActual.PorcentajeRenta = porcRenta;
                    }
                    else if (totalSalarioAnteriorParaRenta >= porcRenta.RangoFinal)  // Si el salario se sobrepasa a este porcentaje, se calcula el excedente
                    {
                        totalRentaAnterior += ((porcRenta.RangoFinal ?? 0) - (porcRenta.RangoInicial ?? 0)) * (porcRenta.PorcRenta ?? 0) / 100;
                    }
                    // else{} En este el salario es inferior y por ende no aplica este porcentaje de renta

                    // Lo mismo para la renta del salario Actual
                    if ((porcRenta.RangoInicial == null || porcRenta.RangoInicial <= totalSalarioActualParaRenta) &&
                        (porcRenta.RangoFinal == null || totalSalarioActualParaRenta <= porcRenta.RangoFinal))
                    {
                        totalRentaActual += (totalSalarioActualParaRenta - (porcRenta.RangoInicial ?? 0)) * (porcRenta.PorcRenta ?? 0) / 100;
                        model.PeriodoActual.PorcentajeRenta = porcRenta;
                    }
                    else if (totalSalarioActualParaRenta >= porcRenta.RangoFinal)
                    {
                        totalRentaActual += ((porcRenta.RangoFinal ?? 0) - (porcRenta.RangoInicial ?? 0)) * (porcRenta.PorcRenta ?? 0) / 100;
                    }
                }
                double totalRentaDiferencia = totalRentaActual - totalRentaAnterior; // Obtener la diferencia de las rentas

                // Obtener el las diferencias del TotalSalario, Renta y SalarioEscolar, de Anterior y Actual
                model.PeriodoActual.TotalSalarioEscolar = salarioEscolarDiferencia;
                model.PeriodoActual.TotalRenta = totalRentaDiferencia;
                model.PeriodoActual.TotalDiferencia = totalSalarioDiferencia;
                model.PeriodoActual.TotalAguinaldo = (model.PeriodoActual.TotalDiferencia + model.PeriodoActual.TotalSalarioEscolar) / 12;

                // Calculo de las deducciones (Obrero y Patronal)
                double subTotal = (model.PeriodoActual.TotalDiferencia ?? 0) + (model.PeriodoActual.TotalSalarioEscolar ?? 0);
                var deduccionesObrero = (List<List<CBaseDTO>>)Session["deduccionesObrero"]; // Lista -> Periodo -> DeduccionDeEsePeriodo
                if (Session["catalogosDeduccionObrero"] == null)
                {
                    Session["catalogosDeduccionObrero"] = new List<CCatalogoTipoDeduccionDTO>();
                }
                var catalogosDeduccionObrero = (List<CCatalogoTipoDeduccionDTO>)Session["catalogosDeduccionObrero"]; // Lista -> CatalogoTipoDeduccionQueAplico en ese periodo
                catalogosDeduccionObrero.Add((CCatalogoTipoDeduccionDTO)deduccionesObrero.ElementAt(0).ElementAt(0)); // Obtiene el catalogo tipo deduccion

                // Sumatoria de los porcentajes de obrero que aplicaron en ese periodo
                double porcTotalObrero = 0.0;
                foreach (CRubroDeduccionDTO deduccion in deduccionesObrero.ElementAt(1))
                {
                    porcTotalObrero += deduccion.PorcRubro ?? 0;
                }

                var deduccionesPatronal = (List<List<CBaseDTO>>)Session["deduccionesPatronales"];
                if (Session["catalogosDeduccionPatronal"] == null)
                {
                    Session["catalogosDeduccionPatronal"] = new List<CCatalogoTipoDeduccionDTO>();
                }
                var catalogosDeduccionPatronal = (List<CCatalogoTipoDeduccionDTO>)Session["catalogosDeduccionPatronal"];
                catalogosDeduccionPatronal.Add((CCatalogoTipoDeduccionDTO)deduccionesPatronal.ElementAt(0).ElementAt(0)); // Obtiene el catalogo tipo deduccion

                // Sumatoria de los porcentajes de patronal que aplicaron en ese periodo
                double porcTotalPatronal = 0.0;
                foreach (CRubroDeduccionDTO deduccion in deduccionesPatronal.ElementAt(1))
                {
                    porcTotalPatronal += (deduccion.PorcRubro ?? 0);
                }

                // Calculo de las deducciones
                model.PeriodoActual.TotalDeduccionesObrero = (porcTotalObrero / 100) * (model.PeriodoActual.TotalDiferencia ?? 0); // Solo toma en cuenta la diferencia
                model.PeriodoActual.TotalDeduccionesPatronal = (porcTotalPatronal / 100) * subTotal; // Subtotal toma en cuenta diferencia y salario escolar

                // Calculo del total a pagar = Diferencia + Salario Escolar - DeduccionesObrero - DeduccionesRenta + Aguinaldo
                double totalAPagarServidor = subTotal - (model.PeriodoActual.TotalDeduccionesObrero ?? 0) - (model.PeriodoActual.TotalRenta ?? 0) + (model.PeriodoActual.TotalAguinaldo ?? 0);

                // REGISTRAR ESTE PERIODO EN LA LISTA DE PERIODOS

                var periodos = (List<CDetalleDiferenciaSalarialDTO>)Session["periodos"];
                periodos.Add(model.PeriodoActual);// Agregar el nuevo periodo
                Session["periodos"] = periodos;

                // REGISTRAR LOS COMPONENTES SALARIALES (DetalleRubro) QUE APLICARON EN ESTE PERIODO

                if (Session["rubrosPorPeriodo"] == null)
                {
                    Session["rubrosPorPeriodo"] = new List<List<CDetalleRubroDTO>>();
                }
                var rubrosPorPeriodo = (List<List<CDetalleRubroDTO>>)Session["rubrosPorPeriodo"];
                rubrosPorPeriodo.Add(rubrosActuales);// Agregar los rubros a lista de listas de rubros por periodo
                Session["rubrosPorPeriodo"] = rubrosPorPeriodo;

                // Limpiar el periodo actual
                model.PeriodoActual = new CDetalleDiferenciaSalarialDTO();
                model.Paso = 2; // Regresar al segundo paso
                ModelState.Clear(); // Limpiar cache
                return View(model);
            }
            catch (Exception error)
            {
                ModelState.AddModelError("buscar", (error.Message));
                return PartialView("_ErrorDiferenciaSalarial");
            }
        }

        /*
            Editar los componentes salariales de un periodo
            Carga todos los componentes de nuevo, y coloca los que ya habia ingresado para editarlos
         */
        public ActionResult EditarPeriodo(FormularioDiferenciaSalarialVM model, string SubmitButton)
        {
            try
            {
                int idxPeriodo = Int32.Parse(SubmitButton.Split('-')[1]);
                var periodos = (List<CDetalleDiferenciaSalarialDTO>)Session["periodos"];
                var rubrosPorPeriodo = (List<List<CDetalleRubroDTO>>)Session["rubrosPorPeriodo"];

                model.PeriodoActual = periodos[idxPeriodo];
                model.Rubros = rubrosPorPeriodo[idxPeriodo];

                // Remueve el detalle y los componentes, para realizar el calculo nuevamente
                periodos.RemoveAt(idxPeriodo);
                rubrosPorPeriodo.RemoveAt(idxPeriodo);

                Session["periodos"] = periodos;
                Session["rubrosPorPeriodo"] = rubrosPorPeriodo;

                var rubros = (CBaseDTO[])Session["rubros"];
                model.AplicaRubro = Enumerable.Repeat(true, model.Rubros.Count()).ToList(); // Agrega en true los componentes sslariales que ya existian
                foreach (CComponenteSalarialDTO rubro in rubros)
                {
                    if (!model.Rubros.Exists(R => R.ComponenteSalarial.DesComponenteSalarial == rubro.DesComponenteSalarial))
                    {
                        model.Rubros.Add(new CDetalleRubroDTO { ComponenteSalarial = rubro });
                        model.AplicaRubro.Add(false);
                    }
                }
                model.Paso = 3;
                ModelState.Clear(); // Limpiar cache
                return View(model);
            }
            catch (Exception error)
            {
                ModelState.AddModelError("buscar", (error.Message));
                return PartialView("_ErrorDiferenciaSalarial");
            }
        }

        // Eliminar un periodo
        public ActionResult EliminarPeriodo(FormularioDiferenciaSalarialVM model, string SubmitButton)
        {
            try
            {
                int idxPeriodo = Int32.Parse(SubmitButton.Split('-')[1]);
                var periodos = (List<CDetalleDiferenciaSalarialDTO>)Session["periodos"];
                var rubrosPorPeriodo = (List<List<CDetalleRubroDTO>>)Session["rubrosPorPeriodo"];

                periodos.RemoveAt(idxPeriodo);
                rubrosPorPeriodo.RemoveAt(idxPeriodo);

                Session["periodos"] = periodos;
                Session["rubrosPorPeriodo"] = rubrosPorPeriodo;
                ModelState.Clear(); // Limpiar cache
                return View(model);
            }
            catch (Exception error)
            {
                ModelState.AddModelError("buscar", (error.Message));
                return PartialView("_ErrorDiferenciaSalarial");
            }
        }

        /*
            Guarda una diferencia salarial con todos sus periodos y deducciones y rubros (Componentes salariales) que aplicaron
         */
        public ActionResult GuardarDiferenciaSalarial(FormularioDiferenciaSalarialVM model)
        {
            try
            {
                if (model.DiferenciaSalarial == null)
                {
                    // Se crea la diferencia Salarial
                    model.DiferenciaSalarial = new CDiferenciaSalarialDTO
                    {
                        Funcionario = model.Funcionario,
                        Clase = model.DetallePuestoActual.Clase,
                        Especialidad = model.DetallePuestoActual.Especialidad,
                        Estado = 1,
                        FecRegistro = DateTime.Now
                    };
                }

                var periodos = (List<CDetalleDiferenciaSalarialDTO>)Session["periodos"];
                var rubrosPorPeriodo = (List<List<CDetalleRubroDTO>>)Session["rubrosPorPeriodo"];
                var catalogosDeduccionObrero = (List<CCatalogoTipoDeduccionDTO>)Session["catalogosDeduccionObrero"];
                var catalogosDeduccionPatronal = (List<CCatalogoTipoDeduccionDTO>)Session["catalogosDeduccionPatronal"];

                // Se realizan los calculos finales de todos los periodos
                double totalAPagar = 0.0;
                /*for(int i = 0; i < periodos.Count(); i++)
                {
                    totalAPagar += periodo.totalAPagar;
                }*/
                model.DiferenciaSalarial.TotalAPagar = totalAPagar;

                // Se guarda la diferencia salarial
                var nuevaDiferencia = servicioDiferenciaSalarial.GuardarDiferenciaSalarial(model.DiferenciaSalarial);
                if (nuevaDiferencia.GetType() == typeof(CErrorDTO))
                {
                    throw new Exception(((CErrorDTO)nuevaDiferencia).MensajeError);
                }
                // Se guardan los periodos
                model.DiferenciaSalarial.IdEntidad = (int)((CRespuestaDTO)nuevaDiferencia).Contenido;
                for (int i = 0; i < periodos.Count(); i++)
                {
                    periodos[i].DiferenciaSalarial = model.DiferenciaSalarial; // Se le pasa la diferencia salarial al detalle
                    var nuevoDetalle = servicioDiferenciaSalarial.GuardarDetalleDiferenciaSalarial(periodos[i]);
                    if (nuevoDetalle.GetType() == typeof(CErrorDTO))
                    {
                        throw new Exception(((CErrorDTO)nuevoDetalle).MensajeError);
                    }
                    // Se guarda el detalle deduccion (Obrero)
                    periodos[i].IdEntidad = (int)((CRespuestaDTO)nuevoDetalle).Contenido;
                    var nuevoDetalleDeduccionObrero = servicioDiferenciaSalarial.GuardarDetalleDeducciones(new CDetalleDeduccionesDTO
                    {
                        FecRegistro = DateTime.Now,
                        DetalleDiferenciaSalarial = periodos[i],
                        CatalogoTipoDeduccion = catalogosDeduccionObrero[i]

                    });
                    if (nuevoDetalleDeduccionObrero.GetType() == typeof(CErrorDTO))
                    {
                        throw new Exception(((CErrorDTO)nuevoDetalleDeduccionObrero).MensajeError);
                    }
                    // Se guarda el detalle deduccion (Patronal)
                    var nuevoDetalleDeduccionPatronal = servicioDiferenciaSalarial.GuardarDetalleDeducciones(new CDetalleDeduccionesDTO
                    {
                        FecRegistro = DateTime.Now,
                        DetalleDiferenciaSalarial = periodos[i],
                        CatalogoTipoDeduccion = catalogosDeduccionPatronal[i]

                    });
                    if (nuevoDetalleDeduccionPatronal.GetType() == typeof(CErrorDTO))
                    {
                        throw new Exception(((CErrorDTO)nuevoDetalleDeduccionPatronal).MensajeError);
                    }

                    // Se guardan los detalle rubros que aplicaron en ese periodo
                    foreach (CDetalleRubroDTO rubro in rubrosPorPeriodo[i])
                    {
                        rubro.DetalleDiferenciaSalarial = periodos[i];
                        var nuevoRubro = servicioDiferenciaSalarial.GuardarDetalleRubro(rubro);
                        if (nuevoRubro.GetType() == typeof(CErrorDTO))
                        {
                            throw new Exception(((CErrorDTO)nuevoRubro).MensajeError);
                        }
                    }
                }
                ModelState.Clear(); // Limpiar cache
                return View(model);
            }
            catch (Exception error)
            {
                ModelState.AddModelError("buscar", (error.Message));
                return PartialView("_ErrorDiferenciaSalarial");
            }
        }

        [HttpPost]
        public ActionResult ResultadoBuscarDiferenciasSalariales(BusquedaDiferenciaSalarialVM model, int? page, string SubmitButton)
        {
            try
            {
                if (Session["diferenciasSalariales"] == null || SubmitButton == "Buscar")
                {
                    var diferenciasSalariales = servicioDiferenciaSalarial.FiltrarDiferenciasSalariales(model.Filtro, model.FecInicio, model.FecFinal);
                    if (diferenciasSalariales.ElementAt(0).GetType() != typeof(CErrorDTO))
                    {
                        foreach (CDiferenciaSalarialDTO diferencia in diferenciasSalariales)
                        {
                            model.Diferencias.Add(diferencia);
                        }
                    }
                    Session["diferenciasSalariales"] = model.Diferencias;
                }
                model.Diferencias = (List<CDiferenciaSalarialDTO>)Session["diferenciasSalariales"];
                model.ItemsPorPagina = 10;
                model.TotalItems = model.Diferencias.Count();
                model.TotalPaginas = (int)Math.Ceiling((double)model.TotalItems / model.ItemsPorPagina);
                model.PaginaActual = (page != null) ? (int)page : 1;
                int inicio = (model.PaginaActual - 1) * model.ItemsPorPagina;
                int cantElementos = model.ItemsPorPagina;
                if (model.PaginaActual == model.TotalPaginas)
                {
                    int residuo = model.TotalItems % model.ItemsPorPagina;
                    cantElementos = (residuo != 0) ? residuo : model.ItemsPorPagina;
                }
                if (0 < model.TotalItems)
                {
                    model.Diferencias = model.Diferencias.GetRange(inicio, cantElementos);
                }
                return PartialView("_ResultadoSearchTipoEstudio", model);
            }
            catch (Exception)
            {
                return PartialView("_ErrorEstudioPuesto");
            }
        }


        #endregion
    }
}