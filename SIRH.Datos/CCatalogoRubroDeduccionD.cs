using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIRH.DTO;
using System.Threading.Tasks;

namespace SIRH.Datos
{
    public class CCatalogoRubroDeduccionD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CCatalogoRubroDeduccionD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Métodos

        public CRespuestaDTO AgregarCatalogoRubroDeduccion(CatalogoRubroDeduccion catalogoRubroDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.CatalogoRubroDeduccion.Add(catalogoRubroDeduccion);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = catalogoRubroDeduccion.PK_CatalogoRubroDeduccion
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

        public CRespuestaDTO ObtenerCatalogoRubroDeduccion(int PK_CatalogoRubroDeduccion)
        {
            CRespuestaDTO respuesta;
            try
            {
                var catalogoRubroDeduccion = entidadBase.CatalogoRubroDeduccion.FirstOrDefault(C => C.PK_CatalogoRubroDeduccion == PK_CatalogoRubroDeduccion);

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
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ninguna Catalogo Rubro Deduccion." }
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

        public CRespuestaDTO ListarCatalogoRubroDeduccion()
        {
            CRespuestaDTO respuesta;
            try
            {
                var catalogoRubrosDeduccion = entidadBase.CatalogoRubroDeduccion.ToList();
                if (catalogoRubrosDeduccion != null && catalogoRubrosDeduccion.Count > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = catalogoRubrosDeduccion
                    };
                    return respuesta;
                }
                else
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = -1,
                        Contenido = new CErrorDTO { MensajeError = "No se encontró ningun catalogo rubro deduccion." }
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
