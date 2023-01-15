using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIRH.DTO;
using System.Threading.Tasks;

namespace SIRH.Datos
{
    public class CPorcentajeRentaD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CPorcentajeRentaD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Métodos

        public CRespuestaDTO AgregarPorcentajeRenta(PorcentajeRenta porcentajeRenta)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.PorcentajeRenta.Add(porcentajeRenta);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = porcentajeRenta.PK_PorcentajeRenta
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

        public CRespuestaDTO ObtenerPorcentajeRenta(int codigo)
        {
            CRespuestaDTO respuesta;
            try
            {
                var porcentajeRenta = entidadBase.PorcentajeRenta.FirstOrDefault(C => C.PK_PorcentajeRenta == codigo);

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
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun porcentaje de renta." }
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
        public CRespuestaDTO ListarPorcentajeRenta()
        {
            CRespuestaDTO respuesta;
            try
            {
                var porcentajeRenta = entidadBase.PorcentajeRenta.ToList();

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
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun porcentaje de renta." }
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

        public CRespuestaDTO BuscarPorcentajeRenta(DateTime fecha)
        {
            CRespuestaDTO respuesta;
            try
            {
                var porcentajes = entidadBase.PorcentajeRenta.Where(P => (P.FecInicio <= fecha && P.FecFinal >= fecha) ||
                (P.FecInicio == null && P.FecFinal >= fecha) || (P.FecInicio <= fecha && P.FecFinal == null)).ToList();

                if (porcentajes != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = porcentajes
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontraron porcentajes de renta." }
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