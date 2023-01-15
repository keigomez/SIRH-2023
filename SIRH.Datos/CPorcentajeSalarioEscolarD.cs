using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIRH.DTO;
using System.Threading.Tasks;

namespace SIRH.Datos
{
    public class CPorcentajeSalarioEscolarD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CPorcentajeSalarioEscolarD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Métodos

        public CRespuestaDTO AgregarPorcentajeSalarioEscolar(PorcentajeSalarioEscolar porcentajeRenta)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.PorcentajeSalarioEscolar.Add(porcentajeRenta);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = porcentajeRenta.PK_PorcentajeSalarioEscolar
                };
                return respuesta;
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

        public CRespuestaDTO ObtenerPorcentajeSalarioEscolar(int codigo)
        {
            CRespuestaDTO respuesta;
            try
            {
                var porcentajeRenta = entidadBase.PorcentajeSalarioEscolar.FirstOrDefault(C => C.PK_PorcentajeSalarioEscolar == codigo);

                if (porcentajeRenta != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = porcentajeRenta
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun porcentaje de salario escolar." }
                    };
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }
        public CRespuestaDTO ListarPorcentajeSalarioEscolar()
        {
            CRespuestaDTO respuesta;
            try
            {
                var porcentajeSalarioEscolar = entidadBase.PorcentajeSalarioEscolar.ToList();

                if (porcentajeSalarioEscolar != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = porcentajeSalarioEscolar
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun porcentaje de salario escolar." }
                    };
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        public CRespuestaDTO BuscarPorcentajeSalarioEscolar(DateTime fecha)
        {
            CRespuestaDTO respuesta;
            try
            {
                var porcentaje = entidadBase.PorcentajeSalarioEscolar.Where(
                    P => P.FecInicio <= fecha && P.FecFinal >= fecha).FirstOrDefault();
                if (porcentaje == null)
                {
                    porcentaje = entidadBase.PorcentajeSalarioEscolar.Where(P =>
                    P.FecInicio <= fecha && P.FecFinal == null ||
                    P.FecInicio == null && P.FecFinal >= fecha).FirstOrDefault();
                }
                if (porcentaje != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = porcentaje
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun porcentaje de salario escolar." }
                    };
                    return respuesta;
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        #endregion
    }
}