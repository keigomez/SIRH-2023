using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Design;
using System.Linq;
using SIRH.Datos;
using SIRH.DTO;


namespace SIRH.Logica
{
    public class CDetalleDeduccionesL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CDetalleDeduccionesL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CDetalleDeduccionesDTO ConvertirDatosDetalleDeduccionesADTO(DetalleDeducciones item)
        {
            var respuesta = new CDetalleDeduccionesDTO
            {
                IdEntidad = item.PK_DetalleDeducciones,
                FecRegistro = item.FecRegistro
            };
            if (item.DetalleDiferenciaSalarial != null)
            {
                respuesta.DetalleDiferenciaSalarial = CDetalleDiferenciaSalarialL.ConvertirDatosDetalleDiferenciaSalarialADTO(item.DetalleDiferenciaSalarial);
            }
            if (item.CatalogoTipoDeduccion != null)
            {
                respuesta.CatalogoTipoDeduccion = CCatalogoTipoDeduccionL.ConvertirDatosCatalogoTipoDeduccionADTO(item.CatalogoTipoDeduccion);
            }

            return respuesta;
        }

        public CBaseDTO GuardarDetalleDeducciones(CDetalleDeduccionesDTO item)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CDetalleDeduccionesD intermedio = new CDetalleDeduccionesD(entidadBase);
                CCatalogoTipoDeduccionD intermedioCatalogoTipoDeduccion = new CCatalogoTipoDeduccionD(entidadBase);
                CDetalleDiferenciaSalarialD intermedioDetalleDiferenciaSalarial = new CDetalleDiferenciaSalarialD(entidadBase);

                DetalleDeducciones detalleNuevo = new DetalleDeducciones
                {
                    FecRegistro = item.FecRegistro
                };

                var catalogoTipoDeduccion = intermedioCatalogoTipoDeduccion.ObtenerCatalogoTipoDeduccion(item.CatalogoTipoDeduccion.IdEntidad);
                if (catalogoTipoDeduccion.Codigo != -1)
                {
                    detalleNuevo.FK_CatalogoTipoDeduccion = ((CatalogoTipoDeduccion)catalogoTipoDeduccion.Contenido).PK_CatalogoTipoDeduccion;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)catalogoTipoDeduccion).Contenido;
                    return respuesta;
                }


                var detalleDiferenciaSalarial = intermedioDetalleDiferenciaSalarial.BuscarDetalleDiferenciaSalarial(item.DetalleDiferenciaSalarial.IdEntidad);
                if (detalleDiferenciaSalarial.Codigo != -1)
                {
                    detalleNuevo.FK_DetalleDiferenciaSalarial = ((DetalleDiferenciaSalarial)detalleDiferenciaSalarial.Contenido).PK_DetalleDiferenciaSalarial;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)detalleDiferenciaSalarial).Contenido;
                    return respuesta;
                }

                respuesta = intermedio.AgregarDetalleDeducciones(detalleNuevo);
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

        public CBaseDTO BuscarDetalleDeducciones(int PK_DetalleDeducciones)
        {
            CBaseDTO respuesta;
            try
            {
                CDetalleDeduccionesD intermedio = new CDetalleDeduccionesD(entidadBase);

                var datos = intermedio.BuscarDetalleDeducciones(PK_DetalleDeducciones);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosDetalleDeduccionesADTO(((DetalleDeducciones)datos.Contenido));
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

        public List<CBaseDTO> BuscarDetalleDeduccionPorDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            List<CBaseDTO> rubros = new List<CBaseDTO>();
            try
            {
                CDetalleDeduccionesD intermedio = new CDetalleDeduccionesD(entidadBase);

                var datos = intermedio.BuscarDetalleDeduccionesPorDetalleDiferenciaSalarial(PK_DetalleDiferenciaSalarial);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<DetalleDeducciones>)datos.Contenido))
                    {
                        rubros.Add(ConvertirDatosDetalleDeduccionesADTO(item));
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