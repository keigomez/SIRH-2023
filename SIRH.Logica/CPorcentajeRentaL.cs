using SIRH.Datos;
using SIRH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRH.Logica
{
    public class CPorcentajeRentaL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CPorcentajeRentaL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CPorcentajeRentaDTO ConvertirDatosPorcentajeRentaADTO(PorcentajeRenta item)
        {
            return new CPorcentajeRentaDTO
            {
                IdEntidad = item.PK_PorcentajeRenta,
                PorcRenta = item.PorcRenta,
                FecInicio = item.FecInicio,
                FecFinal = item.FecFinal,
                RangoInicial = item.RangoInicial,
                RangoFinal = item.RangoFinal,
            };
        }

        public CBaseDTO GuardarPorcentajeRenta(CPorcentajeRentaDTO porcRenta)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CPorcentajeRentaD intermedio = new CPorcentajeRentaD(entidadBase);

                PorcentajeRenta rubroNuevo = new PorcentajeRenta
                {
                    PK_PorcentajeRenta = porcRenta.IdEntidad,
                    PorcRenta = porcRenta.PorcRenta,
                    FecInicio = porcRenta.FecInicio,
                    FecFinal = porcRenta.FecFinal,
                    RangoInicial = porcRenta.RangoInicial,
                    RangoFinal = porcRenta.RangoFinal,
                };
                respuesta = intermedio.AgregarPorcentajeRenta(rubroNuevo);
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

        public List<CBaseDTO> BuscarPorcentajeRenta(DateTime fecha)
        {
            List<CBaseDTO> respuesta = new List<CBaseDTO>();
            try
            {
                CPorcentajeRentaD intermedio = new CPorcentajeRentaD(entidadBase);

                var datos = intermedio.BuscarPorcentajeRenta(fecha);
                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<PorcentajeRenta>)datos.Contenido))
                    {
                        respuesta.Add(ConvertirDatosPorcentajeRentaADTO(item));
                    }
                    return respuesta;
                }
                else
                {
                    throw new Exception(((CErrorDTO)datos.Contenido).MensajeError);
                }
            }
            catch (Exception error)
            {
                respuesta = new List<CBaseDTO> { new CErrorDTO { MensajeError = error.Message } };
                return respuesta;
            }
        }
        #endregion
    }
}

