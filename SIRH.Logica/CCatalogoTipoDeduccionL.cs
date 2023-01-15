using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRH.Datos;
using SIRH.DTO;

namespace SIRH.Logica
{
    public class CCatalogoTipoDeduccionL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CCatalogoTipoDeduccionL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CCatalogoTipoDeduccionDTO ConvertirDatosCatalogoTipoDeduccionADTO(CatalogoTipoDeduccion item)
        {
            var respuesta = new CCatalogoTipoDeduccionDTO
            {
                IdEntidad = item.PK_CatalogoTipoDeduccion,
                FecInicio = item.FecInicio,
                FecFinal = item.FecFinal
            };
            if (item.TipoDeduccion != null)
            {
                respuesta.TipoDeduccion = new CTipoDeduccionDTO
                {
                    IdEntidad = item.TipoDeduccion.PK_TipoDeduccion,
                    DescripcionTipoDeduccion = item.TipoDeduccion.DesTipDeduccion,
                };
            }
            return respuesta;
        }

        public CBaseDTO GuardarCatalogoTipoDeduccion(CCatalogoTipoDeduccionDTO item)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CCatalogoTipoDeduccionD intermedio = new CCatalogoTipoDeduccionD(entidadBase);
                CTipoDeduccionD intermedioTipoDeduccion = new CTipoDeduccionD(entidadBase);

                CatalogoTipoDeduccion rubroNuevo = new CatalogoTipoDeduccion
                {
                    PK_CatalogoTipoDeduccion = item.IdEntidad,
                    FecInicio = item.FecInicio,
                    FecFinal = item.FecFinal
                };

                var tipoDeduccion = intermedioTipoDeduccion.BuscarTipoDeduccion(item.TipoDeduccion.IdEntidad);
                if (tipoDeduccion.Codigo != -1)
                {
                    rubroNuevo.FK_TipoDeduccion = ((TipoDeduccion)tipoDeduccion.Contenido).PK_TipoDeduccion;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)tipoDeduccion).Contenido;
                    return respuesta;
                }

                respuesta = intermedio.AgregarCatalogoTipoDeduccion(rubroNuevo);
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

        public CBaseDTO BuscarCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion)
        {
            CBaseDTO respuesta;
            try
            {
                CCatalogoTipoDeduccionD intermedio = new CCatalogoTipoDeduccionD(entidadBase);

                var datos = intermedio.ObtenerCatalogoTipoDeduccion(PK_CatalogoTipoDeduccion);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosCatalogoTipoDeduccionADTO(((CatalogoTipoDeduccion)datos.Contenido));
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

        // Busca todas las deducciones que aplican a un empleado en el periodo de fecha segun el tipo (obrero, patronal)
        public List<List<CBaseDTO>> BuscarRubrosDeducciones(DateTime fecha, int PK_TipoDeduccion)
        {
            List<List<CBaseDTO>> respuesta = new List<List<CBaseDTO>>();

            CCatalogoTipoDeduccionD intermedio = new CCatalogoTipoDeduccionD(entidadBase);
            CRubroDeduccionD intermedioRubros = new CRubroDeduccionD(entidadBase);

            // Buscar el tipo de deduccion
            var tipo = intermedio.BuscarCatalogoTipoDeduccion(fecha, PK_TipoDeduccion);
            if (tipo != null && tipo.Contenido.GetType() != typeof(CErrorDTO))
            {
                List<CBaseDTO> tipoDeduccion = new List<CBaseDTO>
                {
                    ConvertirDatosCatalogoTipoDeduccionADTO((CatalogoTipoDeduccion)tipo.Contenido)
                };
                respuesta.Add(tipoDeduccion);
            }
            else
            {
                List<CBaseDTO> tipoDeduccion = new List<CBaseDTO>
                {
                    new CErrorDTO { Codigo = -1, MensajeError = "No se encontro el tipo de deduccion" }
                };
                respuesta.Add(tipoDeduccion);
                return respuesta;
            }

            // Buscar los rubros que aplican en ese periodo segun el tipo de rubro
            var datosRubros = intermedioRubros.BuscarDetallesPorCatalogoTipoDeduccion(((CatalogoTipoDeduccion)tipo.Contenido).PK_CatalogoTipoDeduccion);
            List<CBaseDTO> rubros = new List<CBaseDTO>();
            foreach (var item in ((List<RubroDeduccion>)datosRubros.Contenido))
            {
                rubros.Add(CRubroDeduccionL.ConvertirDatosRubroDeduccionADTO(item));
            }
            respuesta.Add(rubros);

            return respuesta;
        }

        #endregion
    }
}