using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIRH.Datos.Helpers;
using SIRH.DTO;

namespace SIRH.Datos
{
    public class CComponentePresupuestarioD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CComponentePresupuestarioD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Metodos

        //__________________________________________DECRETOS________________________________________________________________________

        public CRespuestaDTO AgregarDecretoComponentePresupuestario(ComponentePresupuestario registro)
        {
            bool valorInicial = true;
            CRespuestaDTO respuesta;
            try
            {

                var resultado = entidadBase.ComponentePresupuestario.Include("Programa")
                                        .Include("CatMovimientoPresupuesto")
                                        .Include("ObjetoGasto")
                                        .Where(F => F.AnioPresupuesto == registro.AnioPresupuesto
                                        && F.ObjetoGasto.PK_ObjetoGasto == registro.ObjetoGasto.PK_ObjetoGasto && F.Programa.PK_Programa == registro.Programa.PK_Programa)
                                        .ToList();

                if (resultado.Count() >= 1)
                {
                    valorInicial = true;
                }
                else
                {
                    valorInicial = false;
                }



                if (valorInicial == true)
                {
                    entidadBase.ComponentePresupuestario.Add(registro);
                    entidadBase.SaveChanges();

                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = registro.PK_ComponentePresupuestario
                    };
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = "No existe un registro del presupuesto inicial para el año, programa y objeto de gasto especificados"
                    };
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { Mensaje = error.Message }
                };
            }
            return respuesta;
        }


        //__________________________________________DECRETOS________________________________________________________________________

        public CRespuestaDTO GuardarComponentePresupuestario(ComponentePresupuestario registro)
        {
            bool valorInicial = true;
            CRespuestaDTO respuesta;
            try
            {
                if (registro.CatMovimientoPresupuesto.PK_CatMovimientoPresupuesto == 1)
                {
                    var resultado = entidadBase.ComponentePresupuestario.Include("Programa")
                                            .Include("CatMovimientoPresupuesto")
                                            .Include("ObjetoGasto")
                                            .Where(F => F.AnioPresupuesto == registro.AnioPresupuesto && F.CatMovimientoPresupuesto.PK_CatMovimientoPresupuesto == 1
                                            && F.ObjetoGasto.PK_ObjetoGasto == registro.ObjetoGasto.PK_ObjetoGasto && F.Programa.PK_Programa == registro.Programa.PK_Programa)
                                            .ToList();

                    if (resultado.Count() < 1)
                    {
                        valorInicial = true;
                    }
                    else
                    {
                        valorInicial = false;
                    }
                }


                if (valorInicial == true)
                {
                    entidadBase.ComponentePresupuestario.Add(registro);
                    entidadBase.SaveChanges();

                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = registro.PK_ComponentePresupuestario
                    };
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = "Ya existe un registro del presupuesto inicial para el año, programa y objeto de gasto especificados"
                    };
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { Mensaje = error.Message }
                };
            }
            return respuesta;
        }




        public CRespuestaDTO ObtenerMovimientoPresupuesto(int idMovimiento)
        {
            CRespuestaDTO respuesta;
            try
            {
                var registro = entidadBase.ComponentePresupuestario.Include("Programa")
                                            .Include("CatMovimientoPresupuesto")
                                            .Include("ObjetoGasto")
                                            .Include("ObjetoGasto.Partida")
                                            .Include("ObjetoGasto.SubPartida")
                                            .Where(F => F.PK_ComponentePresupuestario == idMovimiento).FirstOrDefault();

                if (registro != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = registro
                    };
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = "Ocurrió un error al leer los datos del movimiento"
                    };
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
            }

            return respuesta;
        }

        //------BUSQUEDA POR PARAMETRO----------------------------------------------------------
        public CRespuestaDTO ListarComponentePresupuestario()
        {
            try
            {
                var componentePresupuestario = entidadBase.ComponentePresupuestario.ToList();
                if (componentePresupuestario != null)
                {
                    return new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = componentePresupuestario
                    };
                }
                else
                {
                    throw new Exception("No se encontro el componente presupuestario");
                }
            }
            catch (Exception error)
            {
                return new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
            }
        }



        private List<ComponentePresupuestario> BuscarPorParametro(List<ComponentePresupuestario> datosPrevios, object parametro, string elemento)
        {
            string paramString = "";
            int paramInt = 0;
            
            

            if (parametro.GetType() == typeof(String))
            {
                paramString = parametro.ToString();
            }


            if (parametro.GetType() == typeof(Int32))
            {
                paramInt = Convert.ToInt32(parametro);
            }

            if (datosPrevios.Count < 1)
            {
                switch (elemento)
                {
                    case "AnioPresupuesto":

                        datosPrevios = entidadBase.ComponentePresupuestario.Include("Programa")
                                            .Include("CatMovimientoPresupuesto")
                                            .Include("ObjetoGasto")
                                            .Include("MtoComponentePresupuestario")
                                            .Where(F => F.AnioPresupuesto == paramString).ToList();


                        break;
                    case "Programa":
                        datosPrevios = entidadBase.ComponentePresupuestario.Include("AnioPresupuesto")
                                            .Include("CatMovimientoPresupuesto")
                                            .Include("ObjetoGasto")
                                            .Include("MtoComponentePresupuestario")
                                            .Where(F => F.Programa.PK_Programa == paramInt).ToList();
              
                        break;
                    default:
                        datosPrevios = new List<ComponentePresupuestario>();
                        break;
                }
            }
            else
            {
                switch (elemento)
                {
                    case "AnioPresupuesto":
                       // datosPrevios = datosPrevios.Where(C => C.Nombramiento.Funcionario.IdCedulaFuncionario == param).ToList();
                        datosPrevios = datosPrevios.Where(C => C.AnioPresupuesto == paramString).ToList();
                        break;
                    case "Programa":
                        datosPrevios = datosPrevios.Where(C => C.Programa.PK_Programa == paramInt).ToList();
                        break;
                    default:
                        datosPrevios = new List<ComponentePresupuestario>();
                        break;
                }
            }

            return datosPrevios;
        }

        public CRespuestaDTO BuscarPresupuestos(List<ComponentePresupuestario> datosPrevios, object parametro, string elemento)
        {
            CRespuestaDTO respuesta;

            try
            {
                datosPrevios = BuscarPorParametro(datosPrevios, parametro, elemento);
                if (datosPrevios.Count > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = datosPrevios
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { Mensaje = "No se encontraron resultados para los parámetros de búsqueda establecidos" }
                    };
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { Mensaje = error.Message }
                };
                return respuesta;
            }
        }



        //------BUSQUEDA POR PARAMETRO----------------------------------------------------------


        public List<ComponentePresupuestario> ListarComponentePresupuestario(string anno)
        {
            List<ComponentePresupuestario> resultado = new List<ComponentePresupuestario>();

            try
            {
                var componentes = entidadBase.ComponentePresupuestario.Include("Programa")
                                            .Include("CatMovimientoPresupuesto")
                                            .Include("ObjetoGasto")
                                            .Where(F => F.AnioPresupuesto == anno).ToList();

                if (componentes != null)
                {
                    resultado = componentes;
                    return resultado.OrderBy(Q => Q.FK_Programa).OrderBy(Q => Q.FK_ObjetoGasto).ToList();
                }
                else
                {
                    throw new Exception("No se encontraron movimientos de presupuesto para el año especificado.");
                }
            }
            catch (Exception error)
            {
                string r = error.ToString();
            }
            return resultado;
        }



        public List<ComponenteSalarial> CargarComponentesSalariales(CComponenteSalarialD ComponenteSalarial)
        {
            List<ComponenteSalarial> resultados = new List<ComponenteSalarial>();
            resultados = entidadBase.ComponenteSalarial.ToList();
            return resultados;
        }

        public ComponenteSalarial CargarComponenteSalarialId(int idComponenteSalarial)
        {
            ComponenteSalarial resultado = new ComponenteSalarial();
            resultado = entidadBase.ComponenteSalarial.Where(R => R.PK_ComponenteSalarial == idComponenteSalarial).FirstOrDefault();
            return resultado;
        }

        public List<Programa> DescargarProgramas()
        {
            List<Programa> resultados = new List<Programa>();
            resultados = entidadBase.Programa.ToList();
            return resultados;
        }

        public List<ObjetoGasto> DescargarObjetosGasto()
        {
            List<ObjetoGasto> resultados = new List<ObjetoGasto>();
            resultados = entidadBase.ObjetoGasto.ToList();
            return resultados;
        }

        public List<CatMovimientoPresupuesto> DescargarCatMovimientoPresupuesto()
        {
            List<CatMovimientoPresupuesto> resultados = new List<CatMovimientoPresupuesto>();
            resultados = entidadBase.CatMovimientoPresupuesto.ToList();
            return resultados;
        }

        public CRespuestaDTO ActualizarComponentePresupuestario(ComponentePresupuestario componente)
        {
            CRespuestaDTO respuesta;
            int dato;

            try
            {
                //EntityKey llave;
                //object objetoOriginal;

                using (entidadBase)
                {

                    entidadBase.Entry(componente).State = EntityState.Modified;


                    dato = entidadBase.SaveChanges();
                }


                if (dato > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = true
                    };
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = false
                    };
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
            }
            return respuesta;
        }

        public CRespuestaDTO ObtenerDecreto(int codDecreto)
        {
            CRespuestaDTO respuesta;

            try
            {

                var datosDecreto = entidadBase.ComponentePresupuestario.Where(M => M.PK_ComponentePresupuestario == codDecreto).FirstOrDefault();

                if (datosDecreto != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = datosDecreto
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron Decretos asociados a la clave especificada.");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { Mensaje = error.Message }
                };

                return respuesta;
            }
        }

        #endregion

    }
}