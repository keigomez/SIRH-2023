using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Design;
using System.Linq;
using SIRH.Datos;
using SIRH.DTO;

namespace SIRH.Logica
{
    public class CCatalogoRubroDeduccionL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CCatalogoRubroDeduccionL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CCatalogoRubroDeduccionDTO ConvertirDatosCatalogoRubroDeduccionADTO(CatalogoRubroDeduccion item)
        {
            return new CCatalogoRubroDeduccionDTO
            {
                IdEntidad = item.PK_CatalogoRubroDeduccion,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public CBaseDTO GuardarCatalogoRubroDeduccion(CCatalogoRubroDeduccionDTO rubro)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CCatalogoRubroDeduccionD intermedio = new CCatalogoRubroDeduccionD(entidadBase);

                CatalogoRubroDeduccion estudioNuevo = new CatalogoRubroDeduccion
                {
                    Codigo = rubro.Codigo,
                    Descripcion = rubro.Descripcion,

                };
                respuesta = intermedio.AgregarCatalogoRubroDeduccion(estudioNuevo);
                if (((CRespuestaDTO)respuesta).Contenido.GetType() == typeof(CErrorDTO))
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)respuesta).Contenido;
                    return respuesta;
                }

            }
            catch (Exception error)
            {
                respuesta = new CErrorDTO { MensajeError = error.Message };
            }
            return respuesta;
        }

        public CBaseDTO BuscarCatalogoRubroDeduccion(int PK_CatalogoRubroDeduccion)
        {
            CBaseDTO respuesta;
            try
            {
                CCatalogoRubroDeduccionD intermedio = new CCatalogoRubroDeduccionD(entidadBase);

                var datos = intermedio.ObtenerCatalogoRubroDeduccion(PK_CatalogoRubroDeduccion);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosCatalogoRubroDeduccionADTO(((CatalogoRubroDeduccion)datos.Contenido));
                    return respuesta;
                }
                else
                {
                    throw new Exception(((CErrorDTO)datos.Contenido).MensajeError);
                }
            }
            catch (Exception error)
            {
                respuesta = new CErrorDTO { MensajeError = error.Message };
                return respuesta;
            }
        }

        public List<CBaseDTO> ObtenerCatalogoRubrosDeduccion()
        {
            List<CBaseDTO> rubros = new List<CBaseDTO>();
            try
            {
                CCatalogoRubroDeduccionD intermedio = new CCatalogoRubroDeduccionD(entidadBase);

                var datos = intermedio.ListarCatalogoRubroDeduccion();

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<CatalogoRubroDeduccion>)datos.Contenido))
                    {
                        rubros.Add(ConvertirDatosCatalogoRubroDeduccionADTO(item));
                    }
                    return rubros;
                }
                else
                {
                    throw new Exception(((CErrorDTO)datos.Contenido).MensajeError);
                }
            }
            catch (Exception error)
            {
                return new List<CBaseDTO> { new CErrorDTO { MensajeError = error.Message } };
            }
        }

        #endregion
    }
}