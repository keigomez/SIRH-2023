using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIRH.DTO;
using System.Threading.Tasks;

namespace SIRH.Datos
{
    public class CCatalogoTipoDeduccionD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CCatalogoTipoDeduccionD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Métodos

        public CRespuestaDTO AgregarCatalogoTipoDeduccion(CatalogoTipoDeduccion catalogoRubroDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.CatalogoTipoDeduccion.Add(catalogoRubroDeduccion);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = catalogoRubroDeduccion.PK_CatalogoTipoDeduccion
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

        public CRespuestaDTO ObtenerCatalogoTipoDeduccion(int codigo)
        {
            CRespuestaDTO respuesta;
            try
            {
                var catalogoRubroDeduccion = entidadBase.CatalogoTipoDeduccion.FirstOrDefault(C => C.PK_CatalogoTipoDeduccion == codigo);

                if (catalogoRubroDeduccion != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = catalogoRubroDeduccion
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ninguna Catalogo tipo Deduccion." }
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

        public CRespuestaDTO ListarCatalogoTipoDeduccion()
        {
            CRespuestaDTO respuesta;
            try
            {
                var catalogoTipoDeduccion = entidadBase.CatalogoTipoDeduccion.ToList();
                if (catalogoTipoDeduccion != null && catalogoTipoDeduccion.Count > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = catalogoTipoDeduccion
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun catalogo tipo deduccion." }
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

        public CRespuestaDTO BuscarCatalogoTipoDeduccion(DateTime fecha, int PK_TipoDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                var catalogoRubroDeduccion = entidadBase.CatalogoTipoDeduccion.Where(P => P.FK_TipoDeduccion == PK_TipoDeduccion &&
                P.FecInicio <= fecha && P.FecFinal >= fecha).FirstOrDefault();
                if (catalogoRubroDeduccion == null)
                {
                    catalogoRubroDeduccion = entidadBase.CatalogoTipoDeduccion.Where(P =>
                    (P.FK_TipoDeduccion == PK_TipoDeduccion) &&
                    (P.FecInicio <= fecha && P.FecFinal == null ||
                    P.FecInicio == null && P.FecFinal >= fecha)).FirstOrDefault();
                }
                if (catalogoRubroDeduccion != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = catalogoRubroDeduccion
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ninguna Catalogo tipo Deduccion." }
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
