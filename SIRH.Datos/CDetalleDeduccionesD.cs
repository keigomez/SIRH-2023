using System;
using System.Collections.Generic;
using System.Linq;
using SIRH.DTO;

namespace SIRH.Datos
{
    public class CDetalleDeduccionesD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CDetalleDeduccionesD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Metodos

        public CRespuestaDTO AgregarDetalleDeducciones(DetalleDeducciones detalleDeducciones)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.DetalleDeducciones.Add(detalleDeducciones);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = detalleDeducciones.PK_DetalleDeducciones
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

        public CRespuestaDTO BuscarDetalleDeducciones(int PK_DetalleDeducciones)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DetalleDeducciones
                    .Include("CatalogoDetalleDeduciones")
                    .Where(D => D.PK_DetalleDeducciones == PK_DetalleDeducciones)
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
                    throw new Exception("No se encontro ningun detalle deducciones con el id consultado");
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

        public CRespuestaDTO BuscarDetalleDeduccionesPorDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DetalleDeducciones
                    .Include("CatalogoDetalleDeduciones")
                    .Where(D => D.FK_DetalleDiferenciaSalarial == PK_DetalleDiferenciaSalarial)
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
                    throw new Exception("No se encontro ningun detalle deducciones con el id consultado");
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
