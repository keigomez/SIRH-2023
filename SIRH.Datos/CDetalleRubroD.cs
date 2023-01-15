using System;
using System.Collections.Generic;
using System.Linq;
using SIRH.DTO;

namespace SIRH.Datos
{
    public class CDetalleRubroD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CDetalleRubroD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Metodos

        public CRespuestaDTO AgregarDetalleRubro(DetalleRubro detalleRubro)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.DetalleRubro.Add(detalleRubro);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = detalleRubro.PK_DetalleRubro
                };
                return respuesta;
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

        public CRespuestaDTO BuscarDetalleRubro(int PK_DetalleRubro)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DetalleRubro
                    .Include("ComponenteSalarial")
                    .Where(D => D.PK_DetalleRubro == PK_DetalleRubro)
                    .ToList();

                if (lista != null && lista.Count() > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = lista
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontro ningun detalle rubro con el id consultado");
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

        public CRespuestaDTO BuscarRubrosPorDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DetalleRubro
                    .Include("ComponenteSalarial")
                    .Where(D => D.PK_DetalleRubro == PK_DetalleDiferenciaSalarial)
                    .ToList();

                if (lista != null && lista.Count() > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = lista
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontro ningun rubro con el id de detalle diferencia salarial consultado");
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
