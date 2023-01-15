using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Design;
using System.Linq;
using SIRH.Datos;
using SIRH.DTO;


namespace SIRH.Logica
{
    public class CRubroDeduccionL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CRubroDeduccionL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CRubroDeduccionDTO ConvertirDatosRubroDeduccionADTO(RubroDeduccion item)
        {
            var respuesta = new CRubroDeduccionDTO
            {
                IdEntidad = item.PK_RubroDeduccion,
                PorcRubro = item.PorcRubro
            };
            if (item.CatalogoTipoDeduccion != null)
            {
                respuesta.CatalogoTipoDeduccion = CCatalogoTipoDeduccionL.ConvertirDatosCatalogoTipoDeduccionADTO(item.CatalogoTipoDeduccion);
            }
            if (item.CatalogoRubroDeduccion != null)
            {
                respuesta.CatalogoRubroDeduccion = CCatalogoRubroDeduccionL.ConvertirDatosCatalogoRubroDeduccionADTO(item.CatalogoRubroDeduccion);
            }

            return respuesta;
        }

        public CBaseDTO GuardarRubroDeduccion(CRubroDeduccionDTO rubro)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CRubroDeduccionD intermedio = new CRubroDeduccionD(entidadBase);
                CCatalogoTipoDeduccionD intermedioCatalogoTipoDeduccion = new CCatalogoTipoDeduccionD(entidadBase);
                CCatalogoRubroDeduccionD intermedioCatalogoRubroDeduccion = new CCatalogoRubroDeduccionD(entidadBase);

                RubroDeduccion rubroNuevo = new RubroDeduccion
                {
                    PorcRubro = rubro.PorcRubro
                };

                var catalogoTipoDeduccion = intermedioCatalogoTipoDeduccion.ObtenerCatalogoTipoDeduccion(rubro.CatalogoTipoDeduccion.IdEntidad);
                if (catalogoTipoDeduccion.Codigo != -1)
                {
                    rubroNuevo.FK_CatalogoTipoDeduccion = ((CatalogoTipoDeduccion)catalogoTipoDeduccion.Contenido).PK_CatalogoTipoDeduccion;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)catalogoTipoDeduccion).Contenido;
                    return respuesta;
                }


                var catalogoRubroDeduccion = intermedioCatalogoRubroDeduccion.ObtenerCatalogoRubroDeduccion(rubro.CatalogoRubroDeduccion.IdEntidad);
                if (catalogoRubroDeduccion.Codigo != -1)
                {
                    rubroNuevo.FK_CatalogoRubroDeduccion = ((CatalogoRubroDeduccion)catalogoRubroDeduccion.Contenido).PK_CatalogoRubroDeduccion;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)catalogoRubroDeduccion).Contenido;
                    return respuesta;
                }

                respuesta = intermedio.AgregarRubroDeduccion(rubroNuevo);
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

        public CBaseDTO BuscarRubroDeduccion(int PK_RubroDeduccion)
        {
            CBaseDTO respuesta;
            try
            {
                CRubroDeduccionD intermedio = new CRubroDeduccionD(entidadBase);

                var datos = intermedio.BuscarRubroDeduccion(PK_RubroDeduccion);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosRubroDeduccionADTO(((RubroDeduccion)datos.Contenido));
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

        public List<CBaseDTO> BuscarDetallesPorCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion)
        {
            List<CBaseDTO> rubros = new List<CBaseDTO>();
            try
            {
                CRubroDeduccionD intermedio = new CRubroDeduccionD(entidadBase);

                var datos = intermedio.BuscarDetallesPorCatalogoTipoDeduccion(PK_CatalogoTipoDeduccion);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<RubroDeduccion>)datos.Contenido))
                    {
                        rubros.Add(ConvertirDatosRubroDeduccionADTO(item));
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
