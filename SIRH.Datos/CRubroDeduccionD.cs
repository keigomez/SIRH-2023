using System;
using System.Collections.Generic;
using System.Linq;
using SIRH.DTO;

namespace SIRH.Datos
{
    public class CRubroDeduccionD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CRubroDeduccionD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Metodos

        public CRespuestaDTO AgregarRubroDeduccion(RubroDeduccion rubroDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.RubroDeduccion.Add(rubroDeduccion);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = rubroDeduccion.PK_RubroDeduccion
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

        public CRespuestaDTO BuscarDetallesPorCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.RubroDeduccion
                    .Include("CatalogoRubroDeduccion")
                    .Where(D => D.FK_CatalogoTipoDeduccion == PK_CatalogoTipoDeduccion)
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
                    throw new Exception("No se encontraron rubros de deducciones para el catalogo consultado");
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

        public CRespuestaDTO BuscarRubroDeduccion(int PK_RubroDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                var estudioP = entidadBase.RubroDeduccion
                        .Include("CatalogoTipoDeduccion")
                        .Include("CatalogoRubroDeduccion")
                        .Where(P => P.PK_RubroDeduccion == PK_RubroDeduccion)
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
                    throw new Exception("No se encontró el rubro deduccion con el id consultado");
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
