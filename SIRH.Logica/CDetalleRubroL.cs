using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Design;
using System.Linq;
using SIRH.Datos;
using SIRH.DTO;

namespace SIRH.Logica
{
    public class CDetalleRubroL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CDetalleRubroL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CDetalleRubroDTO ConvertirDatosDetalleRubroADTO(DetalleRubro item)
        {
            var respuesta = new CDetalleRubroDTO
            {
                IdEntidad = item.PK_DetalleRubro,
                MontoAnterior = item.MontoAnterior,
                MontoActual = item.MontoActual
            };

            if (item.DetalleDiferenciaSalarial != null)
            {
                respuesta.DetalleDiferenciaSalarial = CDetalleDiferenciaSalarialL.ConvertirDatosDetalleDiferenciaSalarialADTO(item.DetalleDiferenciaSalarial);
            }

            if (item.ComponenteSalarial != null)
            {
                respuesta.ComponenteSalarial = new CComponenteSalarialDTO
                {
                    IdEntidad = item.ComponenteSalarial.PK_ComponenteSalarial,
                    DesComponenteSalarial = item.ComponenteSalarial.DesComponenteSalarial,
                };
            }
            return respuesta;
        }
        public CBaseDTO GuardarDetalleRubro(CDetalleRubroDTO item)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CDetalleRubroD intermedio = new CDetalleRubroD(entidadBase);
                CDetalleDiferenciaSalarialD intermedioDetalleDiferenciaSalarial = new CDetalleDiferenciaSalarialD(entidadBase);
                CComponenteSalarialD intermedioComponenteSalarial = new CComponenteSalarialD(entidadBase);

                DetalleRubro detalleNuevo = new DetalleRubro
                {
                    MontoAnterior = item.MontoAnterior,
                    MontoActual = item.MontoActual

                };

                var componenteSalarial = intermedioComponenteSalarial.CargarComponenteSalarialId(item.ComponenteSalarial.IdEntidad);
                if (componenteSalarial.PK_ComponenteSalarial != -1)
                {
                    detalleNuevo.FK_ComponenteSalarial = componenteSalarial.PK_ComponenteSalarial;
                }
                else
                {
                    return new CErrorDTO { Codigo = -1, Mensaje = "No se encontro ningun componente salarial" };
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

                respuesta = intermedio.AgregarDetalleRubro(detalleNuevo);
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

        public CBaseDTO BuscarDetalleRubro(int PK_DetalleRubro)
        {
            CBaseDTO respuesta;
            try
            {
                CDetalleRubroD intermedio = new CDetalleRubroD(entidadBase);

                var datos = intermedio.BuscarDetalleRubro(PK_DetalleRubro);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosDetalleRubroADTO(((DetalleRubro)datos.Contenido));
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

        public List<CBaseDTO> ObtenerCatalogoRubrosDeduccionPorDetalleDiferencia(int PK_DetalleDiferenciaSalarial)
        {
            List<CBaseDTO> rubros = new List<CBaseDTO>();
            try
            {
                CDetalleRubroD intermedio = new CDetalleRubroD(entidadBase);

                var datos = intermedio.BuscarRubrosPorDetalleDiferenciaSalarial(PK_DetalleDiferenciaSalarial);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<DetalleRubro>)datos.Contenido))
                    {
                        rubros.Add(ConvertirDatosDetalleRubroADTO(item));
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