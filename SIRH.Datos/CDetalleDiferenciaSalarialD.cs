using System;
using System.Collections.Generic;
using System.Linq;
using SIRH.DTO;

namespace SIRH.Datos
{
    public class CDetalleDiferenciaSalarialD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CDetalleDiferenciaSalarialD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Metodos

        public CRespuestaDTO AgregarDetalleDiferenciaSalarial(DetalleDiferenciaSalarial detalleDiferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.DetalleDiferenciaSalarial.Add(detalleDiferenciaSalarial);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = detalleDiferenciaSalarial.PK_DetalleDiferenciaSalarial
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

        public CRespuestaDTO BuscarDetallesPorDiferenciaSalariales(int PK_DiferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DetalleDiferenciaSalarial
                    .Include("DetalleDeducciones")
                    .Include("DetalleDeducciones.CatalogoTipoDeduccion")
                    .Include("DetalleDeducciones.CatalogoTipoDeduccion.RubroDeduccion")
                    .Include("DetalleDeducciones.CatalogoTipoDeduccion.RubroDeduccion.CatalogoRubroDeduccion")
                    .Where(D => D.FK_DiferenciaSalarial == PK_DiferenciaSalarial)
                    .OrderBy(Q => Q.FecInicio)
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
                    throw new Exception("No se encontraron detalles de diferencias salariales para el código de puesto consultado");
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

        public CRespuestaDTO BuscarDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                var estudioP = entidadBase.DetalleDiferenciaSalarial
                        .Include("DetalleDeducciones")
                        .Include("DetalleDeducciones.CatalogoTipoDeduccion")
                        .Include("DetalleDeducciones.CatalogoTipoDeduccion.RubroDeduccion")
                        .Include("DetalleDeducciones.CatalogoTipoDeduccion.RubroDeduccion.CatalogoRubroDeduccion")
                        .Where(P => P.PK_DetalleDiferenciaSalarial == PK_DetalleDiferenciaSalarial)
                        .FirstOrDefault();
                if (estudioP != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = estudioP
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontró el detalle de diferencia salarial con el id consultado");
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
